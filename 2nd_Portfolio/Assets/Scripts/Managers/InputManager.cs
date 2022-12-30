using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    private float m_AxisX;
    public float AxisX
    {
        get { return m_AxisX; }
    }
    private float m_AxisY;
    public float AxisY
    {
        get { return m_AxisY; }
    }

    public delegate void MoveEvent(Vector2 _inputDir);
    public delegate void InputEvent();
    private MoveEvent m_OnMoveEvent = null;
    private MoveEvent m_OnInputEvent = null;
    private InputEvent m_OnDodgeEvent = null;
    private InputEvent m_OnDashEvent = null;
    private InputEvent m_OnAction1Event = null;
    private InputEvent m_OnAction2Event = null;
    private InputEvent m_InputManagerJoinEvent = null;
    private InputEvent m_InputManagerLeftEvent = null;
    private Interaction m_Interaction = null;

    private void Update()
    {
        //m_AxisX = Input.GetAxis("Horizontal");
        //m_AxisY = Input.GetAxis("Vertical");
        //OnMove();
    }

    public void AddOnMoveEvent(MoveEvent _callback)
    {
        //m_OnMoveEvent = _callback;
        m_OnInputEvent = _callback;
    }

    public void AddOnInputEvent(MoveEvent _callback)
    {
        m_OnInputEvent = _callback;
    }

    public void AddOnDodgeEvent(InputEvent _callback)
    {
        m_OnDodgeEvent = _callback;
    }

    public void AddOnDashEvent(InputEvent _callback)
    {
        m_OnDashEvent = _callback;
    }

    public void AddOnAction1Event(InputEvent _callback)
    {
        m_OnAction1Event = _callback;
    }

    public void AddOnAction2Event(InputEvent _callback)
    {
        m_OnAction2Event = _callback;
    }

    public void AddOnInteractionEvent(Interaction _callback)
    {
        m_Interaction = _callback;
    }

    public void AddInputManagerJoinEvent(InputEvent _callback)
    {
        m_InputManagerJoinEvent = _callback;
    }

    public void AddInputManagerLeftEvent(InputEvent _callback)
    {
        m_InputManagerLeftEvent = _callback;
    }

    #region 인풋시스템 이벤트 연결 함수

    public void OnMove()
    {
        if (m_OnMoveEvent != null)
        {
            m_OnMoveEvent(new Vector2(m_AxisX, m_AxisY));
        }
    }

    public void OnInput()
    {
        if (m_OnInputEvent != null)
        {
            m_OnInputEvent(new Vector2(m_AxisX, m_AxisY));
        }
    }

    public void OnInputMove(InputAction.CallbackContext _callback)
    {
        Debug.Log("InputMove 실행!");
        Vector2 inputDir = _callback.ReadValue<Vector2>();

        if (m_OnInputEvent != null)
        {
            m_OnInputEvent(inputDir);
        }
    }

    public void OnDodge(InputAction.CallbackContext _callback)
    {
        Debug.Log("InputManager OnSpace 실행!");
        CheckInputAction(_callback, _started: m_OnDodgeEvent);
    }

    public void OnDash(InputAction.CallbackContext _callback)
    {
        Debug.Log("InputManager OnDash 실행!");
        CheckInputAction(_callback, _started: m_OnDashEvent, _canceled: m_OnDashEvent);
    }

    public void OnAction1(InputAction.CallbackContext _callback)
    {
        Debug.Log("InputManager OnAction1 실행!");
        CheckInputAction(_callback, _performed: m_OnAction1Event);
    }

    public void OnAction2(InputAction.CallbackContext _callback)
    {
        Debug.Log("InputManager OnAction2 실행!");
        CheckInputAction(_callback, _performed: m_OnAction2Event);
    }

    public void OnInteraction(InputAction.CallbackContext _callback)
    {
        if (m_Interaction != null)
            CheckInputAction(_callback, _performed: m_Interaction.InteractionExecute);
    }

    public void InputManagerJoin()
    {
        Debug.Log("InputManager Join 실행!");
        m_InputManagerJoinEvent?.Invoke();
    }

    public void InputManagerLeft()
    {
        Debug.Log("InputManager Left 실행!");
        m_InputManagerLeftEvent?.Invoke();
    }

    #endregion

    private void CheckInputAction(InputAction.CallbackContext _callback, InputEvent _started = null, InputEvent _performed = null, InputEvent _canceled = null)
    {
        switch(_callback.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("started 실행!");
                _started?.Invoke();
                break;
            case InputActionPhase.Performed:
                Debug.Log("performed 실행!");
                _performed?.Invoke();
                break;
            case InputActionPhase.Canceled:
                Debug.Log("canceled 실행!");
                _canceled?.Invoke();
                break;
        }
    }
}
