using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerEvent();
    private GameManagerEvent m_GameManagerEvent = null;
    [SerializeField] private MainSceneManager m_MainSceneManager = null;
    [SerializeField] private InputManager m_InputManager = null;
    [SerializeField] private UIManager m_UIManager = null;
    [SerializeField] private MonsterSpawnPoint m_MonsterSpawnPoint = null;
    [SerializeField] private MapGenerateManager m_MapGenerateManager = null;
    [SerializeField] private Player m_Player = null;
    [SerializeField] private Inventory m_Inventory = null;
    [SerializeField] private ItemManager m_ItemManager = null;
    [SerializeField] private MonsterManager m_MonsterManager = null;
    [SerializeField] private Door m_Door = null;
    private Coroutine m_InitUICoroutine = null;
    public GameObject point = null;

    private int m_level = 3;
    private bool mbIsOnBgmSound = true;
    private bool mbIsOnEffectSound = true;
    private Vector2 m_StartPos;

    // 게임매니저의 이벤트에 실행시킬 초기화 함수들을 저장해놓고
    // 씬이 로드 될때마다 실행 후 
    // 한번만 실행되도 되는것들은 이벤트에서 삭제한다
    public void AddGameManagerEvent(GameManagerEvent _callback)
    {
        m_GameManagerEvent += _callback;
    }

    public void RemoveGameManagerEvent(GameManagerEvent _callback)
    {
        m_GameManagerEvent -= _callback;
    }

    public void RunGameManagerEvent()
    {
        m_GameManagerEvent?.Invoke();
    }

    #region 게임매니저 이밴트에 연결할 함수들
    private void InitMainSceneManager()
    {
        m_MainSceneManager = GameObject.FindObjectOfType<MainSceneManager>();
        if (m_MainSceneManager != null)
        {
            m_MainSceneManager.SceneManagerAwake();
            RemoveGameManagerEvent(InitMainSceneManager);
        }
    }

    private void InitInputManager()
    {
        m_InputManager = GameObject.FindObjectOfType<InputManager>();
        if (m_InputManager != null)
        {
            m_InputManager.InputManagerAwake();
            RemoveGameManagerEvent(InitInputManager);
        }
    }

    private void InitUIManager()
    {
        m_UIManager = GameObject.FindObjectOfType<UIManager>();
        if (m_UIManager != null)
        {
            m_UIManager.AddSceneLoadEvent(m_MainSceneManager.LoadScene);
            m_UIManager.AddGameQuitEvent(QuitGame);
            m_UIManager.UIManagerAwake();
            RemoveGameManagerEvent(InitUIManager);
        }
    }

    private void InitMonsterManager()
    {
        m_MonsterManager = GameObject.FindObjectOfType<MonsterManager>();
        if (m_MonsterManager != null)
        {
            m_MonsterManager.MonsterManagerAwake();
            RemoveGameManagerEvent(InitMonsterManager);
        }
    }

    private void InitMonsterSpawnPoint()
    {
        m_MonsterSpawnPoint = GameObject.FindObjectOfType<MonsterSpawnPoint>();
        if (m_MonsterSpawnPoint != null)
        {
            m_MonsterSpawnPoint.AddSpawnPointEvent(m_MonsterManager.SetSpawnPoint);
            m_MonsterSpawnPoint.MonsterSpawnPointAwake();
            RemoveGameManagerEvent(InitMonsterSpawnPoint);
        }
    }

    private void InitMapGenerateManager()
    {
        m_MapGenerateManager = GameObject.FindObjectOfType<MapGenerateManager>();
        if (m_MapGenerateManager != null)
        {
            m_MapGenerateManager.AddMapGenerateEvent(SetPlayerPosition);

            m_MapGenerateManager.AddLoadSceneEvent(m_MainSceneManager.LoadScene);
            m_MapGenerateManager.AddRoomPointEvent(m_MonsterSpawnPoint.SetRoomPoint);
            m_MapGenerateManager.AddRoomManagerSelectMonsterEvent(m_MonsterManager.SummonMonster);
            m_MapGenerateManager.AddRoomManagerRandomMonsterEvent(m_MonsterManager.SummonRandomMonster);
            m_MapGenerateManager.AddRemoveAllMonsterEvent(m_MonsterManager.RemoveAllMonster);
            m_MapGenerateManager.AddRemoveAllDropItemEvent(m_ItemManager.RemoveAllDropItem);
            m_MapGenerateManager.AddInitNodeEvent(m_MonsterManager.AStar.InitNode);
            m_MapGenerateManager.AddRoomManagerChestEvent(m_ItemManager.RandomPopChest);
            m_MapGenerateManager.AddInitDoorEvent(InitDoor);
            m_MapGenerateManager.MapGenerateManagerAwake();

            m_Player.transform.position = m_MapGenerateManager.GetStartPos() + (Vector2.up * 3f);
            StartCoroutine(UpdatePlayerPos());
            RemoveGameManagerEvent(InitMapGenerateManager);
        }
    }

    private void InitInventory()
    {
        m_Inventory = GameObject.FindObjectOfType<Inventory>();
        if (m_Inventory != null)
        {
            m_Inventory.InventoryAwake();
            RemoveGameManagerEvent(InitInventory);
        }
    }

    private void InitPlayer()
    {
        m_Player = GameObject.FindObjectOfType<Player>();
        if (m_Player != null)
        {
            m_Player.PlayerAwake();
            ConectPlayerEvent();
            if (m_UIManager != null)
            {
                m_UIManager.ActiveMainUI(true);
                m_UIManager.AddInitPlayerEvent(m_Player.InitPlayer);
                m_UIManager.InitPlayerEvent();
                m_Player.AddGameOverUIEvent(m_UIManager.ActiveGameOver);
                StartCoroutine(UpdateUIManager_MainUI());
            }
            if (m_Inventory != null)
            {
                m_Player.SetInventory(m_Inventory);
            }
            RemoveGameManagerEvent(InitPlayer);
        }
        //m_UIManager.ActiveMainUI(false);
    }

    private void InitItemManager()
    {
        m_ItemManager = GameObject.FindObjectOfType<ItemManager>();
        if (m_ItemManager != null)
        {
            m_ItemManager.ItemManagerAwake();
            if (m_Inventory != null)
                m_ItemManager.SetInventory(m_Inventory);
            RemoveGameManagerEvent(InitItemManager);
        }
    }


    #endregion
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene _scene, LoadSceneMode _mod)
    {
        Debug.Log("OnSceneLoaded 실행" + "씬 :" + _scene.name);
        NullCheck();
        RunGameManagerEvent();
        SetGameManager();
        if (m_MainSceneManager != null)
        {
            switch (_scene.name)
            {
                case "StartScene":
                    m_MainSceneManager.LoadScene("MainMenuScene");
                    break;
                case "InitScene_MainMenuToTown":
                    m_MainSceneManager.LoadScene("TownScene2");
                    break;
            }
            //if (_scene.name.Equals("TownScene"))
            //    m_InputManager.AddOnOptionEvent(m_UIManager.OnTownOptionEvent);
            //if (_scene.name.Equals("DungeonScene"))
            //    m_InputManager.AddOnOptionEvent(m_UIManager.OnDugneonOptionEvent);
        }
        if (m_Player != null && GameObject.FindGameObjectWithTag("START") != null)
        {
            m_Player.SetPlayerPosition(GameObject.FindGameObjectWithTag("START").transform.position);
        }
    }

    private void NullCheck()
    {
        if (m_MainSceneManager == null)
            AddGameManagerEvent(InitMainSceneManager);
        if (m_InputManager == null)
            AddGameManagerEvent(InitInputManager);
        if (m_UIManager == null)
            AddGameManagerEvent(InitUIManager);
        if (m_ItemManager == null)
            AddGameManagerEvent(InitItemManager);
        if (m_MonsterManager == null)
            AddGameManagerEvent(InitMonsterManager);
        if (m_MonsterSpawnPoint == null)
            AddGameManagerEvent(InitMonsterSpawnPoint);
        if (m_MapGenerateManager == null)
            AddGameManagerEvent(InitMapGenerateManager);
        if (m_Inventory == null)
            AddGameManagerEvent(InitInventory);
        if (m_Player == null)
            AddGameManagerEvent(InitPlayer);
    }

    private void SetGameManager()
    {
        if (m_UIManager != null)
            m_UIManager.UIManagerStart();
        if (m_MonsterManager != null)
            m_MonsterManager.MonsterManagerStart();
        InitDoor();
    }

    private void InitDoor()
    {
        m_Door = null;
        if (m_Door == null)
        {
            m_Door = GameObject.FindObjectOfType<Door>();
            if (m_Door != null)
            {
                m_Door.AddDoorEvent(m_UIManager.OnLoadSceneEvent);
                m_Door.AddMonsterListEvent(m_MonsterManager.GetMonsterListCount);
                m_Door.DoorAwake();
            }
        }
    }

    private void ConectPlayerEvent()
    {
        m_InputManager.AddOnInputEvent(m_Player.OnMoveCallback);
        m_InputManager.AddOnDashEvent(m_Player.OnDashCallback);
        m_InputManager.AddOnDodgeEvent(m_Player.OnDodgeCallback);
        m_InputManager.AddOnAction1Event(m_Player.OnAction1Callback);
        m_InputManager.AddOnAction2Event(m_Player.OnAction2Callback);
        m_Player.AddInteracEvent(SetInetraction);
    }

    public void SetInetraction(Interaction _interac)
    {
        m_InputManager.AddOnInteractionEvent(_interac);
    }

    private void SetIsBgm(bool _bool)
    {
        mbIsOnBgmSound = _bool;
    }

    private void SetIsEffect(bool _bool)
    {
        mbIsOnEffectSound = _bool;
    }

    public void SetPlayerPosition()
    {
        m_Player.SetPlayerPosition(m_MapGenerateManager.GetStartPos());
    }

    public void QuitGame()
    {
        //유니티 에디터 사용중일시
        //UnityEditor.EditorApplication.isPlaying = false;
        //빌드 시
        Application.Quit();
    }

    IEnumerator UpdateUIManager_MainUI()
    {
        while (true)
        {
            m_UIManager.UpdateMainUI(m_Player.GetCurrHp(), m_Player.GetMaxHp(), m_Player.GetCurrSp(), m_Player.GetMaxSp());
            yield return null;
        }
    }

    IEnumerator UpdatePlayerPos()
    {
        while(true)
        {
            if (m_MapGenerateManager == null)
            {
                yield break;
            }
            m_MapGenerateManager.SetRoomManagerTargetPos(m_Player.GetPosition());
            yield return null;
        }
    }    
}
