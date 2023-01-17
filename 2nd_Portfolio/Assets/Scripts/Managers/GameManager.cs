using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player m_Player = null;
    [SerializeField] private InputManager m_InputManager = null;
    [SerializeField] private Inventory m_Inventory = null;
    [SerializeField] private UIManager m_UIManager = null;
    [SerializeField] private ItemManager m_ItemManager = null;
    [SerializeField] private MonsterManager m_MonsterManager = null;

    private int m_level = 3;

    private void Awake()
    {
        InitConectEvent();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(InitGameManager());
    }

    private void InitConectEvent()
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

    IEnumerator InitGameManager()
    {
        while(true)
        {
            m_UIManager.InitUIManager(m_Player.GetCurrHp(), m_Player.GetMaxHp(), m_Player.GetCurrSp(), m_Player.GetMaxSp());
            yield return null;
        }
    }
}
