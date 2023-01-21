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

    // ���ӸŴ����� �̺�Ʈ�� �����ų �ʱ�ȭ �Լ����� �����س���
    // ���� �ε� �ɶ����� ���� �� 
    // �ѹ��� ����ǵ� �Ǵ°͵��� �̺�Ʈ���� �����Ѵ�
    // �׷��� �ϸ� �Ź� ������ ���� �˻縦 �� �ʿ䵵 ���� ����ϰ� ��������
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

    #region ���ӸŴ��� �̹�Ʈ�� ������ �Լ���
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
        Debug.Log("SetGameManager ����");
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
        //����Ƽ ������ ������Ͻ�
        //UnityEditor.EditorApplication.isPlaying = false;
        //���� ��
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
