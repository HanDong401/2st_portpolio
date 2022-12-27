using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class Player : Unit
{
    [SerializeField] private CameraSet m_Camera = null;
    private PlayerMove m_PlayerMove = null;
    private PlayerAction m_PlayerAction = null;
    private PlayerEffect m_PlayerEffect = null;

    private void Awake()
    {
        m_PlayerMove = this.GetComponent<PlayerMove>();
        m_PlayerAction = this.GetComponent<PlayerAction>();
        m_PlayerEffect = this.GetComponent<PlayerEffect>();
    }

    private void Update()
    {
        m_PlayerEffect.InitDust(m_PlayerMove.GetSpeed());
        m_Camera.GetTarget(transform.position);
    }

    public void OnMoveCallback(Vector2 _inputDir)
    {
        m_PlayerMove.OnMove(_inputDir);
        if (_inputDir != Vector2.zero)
            m_PlayerEffect.SetDust(true);
        else
            m_PlayerEffect.SetDust(false);
    }    

    public void OnDashCallback()
    {
        m_PlayerMove.OnDash();
    }

    public void OnDodgeCallback()
    {
        m_PlayerMove.OnDodge();
    }
}
