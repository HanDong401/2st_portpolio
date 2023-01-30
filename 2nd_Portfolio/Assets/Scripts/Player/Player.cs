using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private PlayerAnimation m_PlayerAnimation = null;
    private Animator m_PlayerAnim = null;
    private Rigidbody2D m_PlayerRigid = null;
    private Collider2D m_PlayerColl = null;
    private Inventory m_Inventory = null;
    private bool mbIsInterac = false;
    private bool mbIsCanMove = false;
    private Coroutine m_PlayerUpdateCoroutine = null;
    private Coroutine m_PlayerMoveCoroutine = null;
    [SerializeField] private int m_CurrSp { get; set; }
    [SerializeField] private int m_MaxSp { get; set; }
    [SerializeField] private CameraSet m_Camera = null;
    //private PlayerBaseState m_CurrState = null;

    public void PlayerAwake()
    {
        DontDestroyOnLoad(this.gameObject);
        m_PlayerMove = this.GetComponentInChildren<PlayerMove>();
        m_PlayerMove.AddDodgeEvent(SetLayer);
        m_PlayerAnim = this.GetComponentInChildren<Animator>();
        m_PlayerAnimation = this.GetComponentInChildren<PlayerAnimation>();
        m_PlayerAction = this.GetComponent<PlayerAction>();
        m_PlayerEffect = this.GetComponent<PlayerEffect>();
        m_PlayerRigid = this.GetComponent<Rigidbody2D>();
        m_PlayerColl = this.GetComponent<Collider2D>();
        StartCoroutine(SetInit());
        PlayerStart();
    }

    IEnumerator SetInit()
    {
        while(true)
        {
            if (m_PlayerAnim != null && m_PlayerRigid != null)
            {
                m_PlayerMove.SetAnim(m_PlayerAnim);
                m_PlayerMove.SetRigid(m_PlayerRigid);
                m_PlayerAnimation.SetAnim(m_PlayerAnim);
                yield break;
            }
            yield return null;
        }
    }
    private void PlayerStart()
    {
        SetMaxHp(100);
        SetCurrHp(m_MaxHp);
        SetMaxSp(3);
        SetCurrSp(m_MaxSp);
        m_PlayerUpdateCoroutine = StartCoroutine(PlayerUpdate());
        mbIsCanMove = true;
    }

    private void FixedUpdate()
    {
        if (mbIsCanMove.Equals(true))
        {
            m_PlayerMove.UpdateVelocity();
        }
    }
    #region 콜백함수들
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

    private Coroutine m_DodgeCoroutine = null;
    public void OnDodgeCallback()
    {
        if (m_CurrSp != 0 && m_PlayerMove.GetIsDodge() == false)
        {
            m_PlayerMove.OnDodge();
            --m_CurrSp;
            if (m_DodgeCoroutine != null)
            {
                StopCoroutine(m_DodgeCoroutine);
            }
            m_DodgeCoroutine = StartCoroutine(SpDelay());
        }
    }

    public void OnAction1Callback()
    {
        m_PlayerAction.OnAction1(m_Inventory.GetAction(1));
    }

    public void OnAction2Callback()
    {
        m_PlayerAction.OnAction2(m_Inventory.GetAction(2));
    }

    #endregion

    #region 상호작용 관련

    public void AddInteracEvent(InteracEvent _callback)
    {
        this.m_InteracEvent = _callback;
    }

    private void GetInteraction(Interaction _interac)
    {
        if (m_InteracEvent != null)
            m_InteracEvent(_interac);
    }

    #endregion

    #region 값 전달 함수들

    public int GetCurrSp()
    {
        return m_CurrSp;
    }

    public int GetMaxSp()
    {
        return m_MaxSp;
    }

    public void SetCurrSp(int _currSp)
    {
        this.m_CurrSp = _currSp;
    }

    public void SetMaxSp(int _maxSp)
    {
        this.m_MaxSp = _maxSp;
    }

    public bool GetIsFlipX()
    {
        return m_PlayerMove.GetBoolFlipX();
    }

    public Vector2 GetPosition()
    {
        return this.transform.position;
    }

    public Animator GetAnim()
    {
        return m_PlayerAnim;
    }

    public void SetInventory(Inventory _inven)
    {
        m_Inventory = _inven;
    }

    public void SetPlayerPosition(Vector2 _pos)
    {
        this.transform.position = _pos;
    }
    #endregion

    public void Knockback(Vector2 _dir, int _damage)
    {
        if (m_PlayerMove.GetIsDodge().Equals(true)) return;
        if (m_PlayerMoveCoroutine != null)
            StopCoroutine(m_PlayerMoveCoroutine);
        Vector2 moveDir = ((Vector2)transform.position - _dir).normalized;
        m_PlayerRigid.velocity = Vector2.zero;
        m_PlayerRigid.AddForce(moveDir * _damage, ForceMode2D.Impulse);
        m_PlayerMoveCoroutine = StartCoroutine(PlayerMoveUpdate());
    }

    public void PlayerDamage(int _damage)
    {
        if (m_PlayerMove.GetIsDodge().Equals(true)) return;
        base.OnDamage(_damage);
    }
    
    public void SetLayer(int _layer)
    {
        this.gameObject.layer = _layer;
    }

    //public void ChangeState(string _state)
    //{
    //    switch (_state)
    //    {
    //        case "Idle":
    //            m_CurrState = new PlayerIdleState(this);
    //            break;
    //        case "Move":
    //            m_CurrState = new PlayerMoveState(this);
    //            break;
    //        case "Dash":
    //            m_CurrState = new PlayerDashState(this);
    //            break;
    //        case "Dodge":
    //            m_CurrState = new PlayerDodgeState(this);
    //            break;
    //        case "Hit":
    //            m_CurrState = new PlayerHitState(this);
    //            break;
    //        case "Action1":
    //            m_CurrState = new PlayerAction1State(this);
    //            break;
    //        case "Action2":
    //            m_CurrState = new PlayerAction2State(this);
    //            break;
    //        case "Death":
    //            m_CurrState = new PlayerDeathState(this);
    //            break;
    //    }
    //}


    // 캐릭터가 상호작용 가능한 오브젝트에 닿으면 그 오브젝트의 실행함수를 InputManager의 상호작용 키와 연결
    private void OnTriggerStay2D(Collider2D coll)
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

    IEnumerator SpDelay()
    {
        while (true)
        {
            if (m_CurrSp < m_MaxSp)
            {
                yield return new WaitForSeconds(2f);
                ++m_CurrSp;
            }
            else if (m_CurrSp == m_MaxSp)
                break;
        }
    }

    IEnumerator PlayerUpdate()
    {
        while(true)
        {
            m_PlayerEffect.InitDust(m_PlayerMove.GetSpeed());
            m_Camera.GetTarget(transform.position);
            Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0);
            m_Camera.CameraMove();
            yield return null;
        }
    }

    IEnumerator PlayerMoveUpdate()
    {
        mbIsCanMove = false;
        yield return new WaitForSeconds(0.5f);
        mbIsCanMove = true;
        //while(true)
        //{
        //    m_PlayerMove.UpdateVelocity();
        //    yield return null;
        //}
    }
}
