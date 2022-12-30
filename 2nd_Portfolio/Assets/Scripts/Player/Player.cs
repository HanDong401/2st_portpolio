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
    public delegate void InteracEvent(Interaction _interac);
    private InteracEvent m_InteracEvent = null;
    private PlayerMove m_PlayerMove = null;
    private PlayerAction m_PlayerAction = null;
    private PlayerEffect m_PlayerEffect = null;
    private PlayerInven m_PlayerInven = null;
    private PlayerAnimation m_PlayerAnimation = null;
    private Animator m_PlayerAnim = null;
    private bool mbIsInterac = false;
    private bool mbIsAction = false;
    [SerializeField] private CameraSet m_Camera = null;

    private void Awake()
    {
        m_PlayerMove = this.GetComponent<PlayerMove>();
        m_PlayerAction = this.GetComponent<PlayerAction>();
        m_PlayerEffect = this.GetComponent<PlayerEffect>();
        m_PlayerInven = this.GetComponentInChildren<PlayerInven>();
        m_PlayerAnimation = this.GetComponent<PlayerAnimation>();
        m_PlayerAnim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        m_PlayerEffect.InitDust(m_PlayerMove.GetSpeed());
        m_Camera.GetTarget(transform.position);
        m_PlayerAnim.runtimeAnimatorController = m_PlayerAnimation.GetAnim();
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

    public void OnAction1Callback()
    {
        m_PlayerAction.OnAction1(Inventory.Instance.GetAction(1));
    }

    public void OnAction2Callback()
    {
        m_PlayerAction.OnAction2(Inventory.Instance.GetAction(2));
    }

    public void AddInteracEvent(InteracEvent _callback)
    {
        this.m_InteracEvent = _callback;
    }

    private void GetInteraction(Interaction _interac)
    {
        if (m_InteracEvent != null)
            m_InteracEvent(_interac);
    }

    private void ResetAction()
    {
        m_PlayerAnimation.ResetAnim(m_PlayerAnim);
    }

    // 캐릭터가 상호작용 가능한 오브젝트에 닿으면 그 오브젝트의 실행함수를 InputManager의 상호작용 키와 연결
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.TryGetComponent(out Interaction _interac))
        {
            if (mbIsInterac == false)
            {
                GetInteraction(_interac);
                mbIsInterac = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (mbIsInterac == true)
        {
            GetInteraction(null);
            mbIsInterac = false;
        }
    }
}
