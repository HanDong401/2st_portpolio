using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player m_Player = null;
    [SerializeField] private InputManager m_InputManager = null;
    [SerializeField] private Inventory m_Inventory = null;
    [SerializeField] private UIManager m_UIManager = null;
    [SerializeField] private ItemManager m_ItemManager = null;
    [SerializeField] private MonsterManager m_MonsterManager = null;
    [SerializeField] private MainSceneManager m_MainSceneManager = null;
    [SerializeField] private TeleportTile m_TeleportTile = null;
    private Coroutine m_InitUICoroutine = null;

    private int m_level = 3;
    private bool mbIsOnBgmSound = true;
    private bool mbIsOnEffectSound = true;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetGameManager;
    }

    public void SetGameManager(Scene _scene, LoadSceneMode _mod)
    {
        Debug.Log("SetGameManager 실행");
        DontDestroyOnLoad(this.gameObject);
        SetComponent();
    }

    private void SetComponent()
    {
        if (m_MainSceneManager == null)
            m_MainSceneManager = GameObject.FindObjectOfType<MainSceneManager>();
        if (m_UIManager == null)
        {
            m_UIManager = GameObject.FindObjectOfType<UIManager>();
            m_UIManager.AddMainStartEvent(m_MainSceneManager.LoadScene);
            m_UIManager.AddMainExitEvent(QuitGame);
            m_UIManager.UIManagerAwake();
        }
        else
        {
            m_UIManager.UIManagerStart();
        }
        if (m_Player == null)
        {
            m_Player = GameObject.FindObjectOfType<Player>();
            if (m_Player != null)
            {
                m_Player.PlayerInit();
                ConectPlayerEvent();
                m_UIManager.ActiveMainUI(true);
                //m_UIManager.ActiveInventory(true);
                StartCoroutine(InitUIManager());
                return;
            }
            m_UIManager.ActiveMainUI(false);
            //m_UIManager.ActiveInventory(false);
        }
        //if (m_TeleportTile == null)
        //{
        //    m_TeleportTile = GameObject.FindObjectOfType<TeleportTile>();
        //    if (m_TeleportTile != null)
        //    {
        //        m_TeleportTile.TeleAwake();
        //    }
        //}
        //else
        //{
        //    m_TeleportTile.TeleStart();
        //}
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

    IEnumerator InitUIManager()
    {
        while(true)
        {
            m_UIManager.InitUIManager(m_Player.GetCurrHp(), m_Player.GetMaxHp(), m_Player.GetCurrSp(), m_Player.GetMaxSp());
            yield return null;
        }
    }
}
