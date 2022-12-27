using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player m_Player = null;
    [SerializeField] InputManager m_InputManager = null;
    [SerializeField] MapManager m_MapManager = null;

    private int m_level = 3;

    private void Awake()
    {
        InitInputManager();
        DontDestroyOnLoad(this.gameObject);
        //m_MapManager.SetUpScene(m_level);
    }

    private void InitInputManager()
    {
        m_InputManager.AddOnInputEvent(m_Player.OnMoveCallback);
        m_InputManager.AddOnDashEvent(m_Player.OnDashCallback);
        m_InputManager.AddOnDodgeEvent(m_Player.OnDodgeCallback);
    }
}
