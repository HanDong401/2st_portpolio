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
    [SerializeField] private Player m_Player = null;
    [SerializeField] private InputManager m_InputManager = null;
    [SerializeField] private Inventory m_Inventory = null;
    [SerializeField] private UIManager m_UIManager = null;
    [SerializeField] private ItemManager m_ItemManager = null;
    [SerializeField] private MonsterManager m_MonsterManager = null;
    [SerializeField] private MainSceneManager m_MainSceneManager = null;
    [SerializeField] private MapGenerateManager m_MapGenerateManager = null;
    [SerializeField] private TownToGoDungeon m_TownToGoDungeon = null;
    private Coroutine m_InitUICoroutine = null;

    private int m_level = 3;
    private bool mbIsOnBgmSound = true;
    private bool mbIsOnEffectSound = true;

    // 게임매니저의 이벤트에 실행시킬 초기화 함수들을 저장해놓고
    // 씬이 로드 될때마다 실행 후 
    // 한번만 실행되도 되는것들은 이벤트에서 삭제한다
    // 그렇게 하면 매번 돌릴때 조건 검사를 할 필요도 없고 깔끔하게 없어진다
    public void AddGameManagerEvent(GameManagerEvent _callback)
    {
        m_GameManagerEvent += _callback;
    }

    public void RemoveGameManagerEvnet(GameManagerEvent _callback)
    {
        m_GameManagerEvent -= _callback;
    }

    public void RunGameManagerEvnet()
    {
        m_GameManagerEvent?.Invoke();
    }

    #region 게임매니저 이밴트에 연결할 함수들
    public void SetPlayerPosition()
    {
        m_Player.SetPlayerPosition(m_MapGenerateManager.GetStartPos());
    }
    private void InitMainSceneManager()
    {
        if (m_MainSceneManager == null)
        {
            m_MainSceneManager = GameObject.FindObjectOfType<MainSceneManager>();
            m_MainSceneManager.SceneManagerAwake();
        }
        if (m_MainSceneManager != null)
            RemoveGameManagerEvnet(InitMainSceneManager);
    }

    private void InitUIManager()
    {
        if (m_UIManager == null)
        {
            m_UIManager = GameObject.FindObjectOfType<UIManager>();
            m_UIManager.AddMainStartEvent(m_MainSceneManager.LoadScene);
            m_UIManager.AddMainExitEvent(QuitGame);
            m_UIManager.UIManagerAwake();
        }
        if (m_UIManager != null)
            RemoveGameManagerEvnet(InitUIManager);
    }

    private void InitPlayer()
    {
        if (m_Player == null)
        {
            m_Player = GameObject.FindObjectOfType<Player>();
            if (m_Player != null)
            {
                m_Player.PlayerInit();
                ConectPlayerEvent();
                m_UIManager.ActiveMainUI(true);
                //m_UIManager.ActiveInventory(true);
                //StartCoroutine(InitUIManager());
                //return;
            }
            m_UIManager.ActiveMainUI(false);
            //m_UIManager.ActiveInventory(false);
        }
    }
    #endregion
    private void Awake()
    {
        AddGameManagerEvent(InitMainSceneManager);
        AddGameManagerEvent(InitUIManager);
        AddGameManagerEvent(InitPlayer);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetGameManager;
    }

    public void SetGameManager(Scene _scene, LoadSceneMode _mod)
    {
        Debug.Log("SetGameManager 실행");
        DontDestroyOnLoad(this.gameObject);
        RunGameManagerEvnet();
        SetComponent();
    }

    private void SetComponent()
    {
        m_UIManager.UIManagerStart();
        if (m_MapGenerateManager == null)
        {
            m_MapGenerateManager = GameObject.FindObjectOfType<MapGenerateManager>();
            if (m_MapGenerateManager != null)
            {
                m_MapGenerateManager.AddMapGenerateEvent(SetPlayerPosition);
                m_Player.transform.position = m_MapGenerateManager.GetStartPos() + (Vector2.up * 3f);
            }
        }
        else
        {
            m_Player.transform.position = (Vector2)m_Player.transform.position + m_MapGenerateManager.GetStartPos();
        }
        if (m_TownToGoDungeon == null)
        {
            m_TownToGoDungeon = GameObject.FindObjectOfType<TownToGoDungeon>();
            if (m_TownToGoDungeon != null)
            {
                m_TownToGoDungeon.AddDoorEvent(SetDoor);
            }
        }
    }

    private void SetDoor(string _name)
    {
        m_MainSceneManager.LoadScene(_name);
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

    private void QuitGame()
    {
        //유니티 에디터 사용중일시
        //UnityEditor.EditorApplication.isPlaying = false;
        //빌드 시
        Application.Quit();
    }

    //IEnumerator InitUIManager()
    //{
    //    while(true)
    //    {
    //        m_UIManager.InitUIManager(m_Player.GetCurrHp(), m_Player.GetMaxHp(), m_Player.GetCurrSp(), m_Player.GetMaxSp());
    //        yield return null;
    //    }
    //}
}
