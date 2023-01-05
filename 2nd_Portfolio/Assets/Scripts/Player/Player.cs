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
    private Animator m_PlayerAnim = null;
    private PlayerAnimation m_PlayerAnimation = null;
    private bool mbIsInterac = false;
    private bool mbIsMove = false;
    [field : SerializeField] private int m_CurrSp { get; set; }
    [field : SerializeField] private int m_MaxSp { get; set; }
    [SerializeField] private CameraSet m_Camera = null;

    private void Awake()
    {
        m_PlayerMove = this.GetComponent<PlayerMove>();
        m_PlayerAction = this.GetComponent<PlayerAction>();
        m_PlayerEffect = this.GetComponent<PlayerEffect>();
        m_PlayerAnimation = this.GetComponent<PlayerAnimation>();
        m_PlayerAnim = this.GetComponent<Animator>();
        m_PlayerMove.SetAnim(m_PlayerAnim);
        StartCoroutine(SetInit());
    }

    IEnumerator SetInit()
    {
        while(true)
        {
            if (m_PlayerAnimation.GetAnim() != null)
            {
                m_PlayerMove.SetAnim(m_PlayerAnimation.GetAnim());
                yield break;
            }
            yield return null;
        }
    }
    private void Start()
    {
        SetMaxHp(100);
        SetCurrHp(m_MaxHp);
        SetMaxSp(3);
        SetCurrSp(m_MaxSp);
    }

    private void Update()
    {
        m_PlayerEffect.InitDust(m_PlayerMove.GetSpeed());
        m_Camera.GetTarget(transform.position);
        Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0);
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
        m_PlayerAction.OnAction1(Inventory.Instance.GetAction(1));
    }

    public void OnAction2Callback()
    {
        m_PlayerAction.OnAction2(Inventory.Instance.GetAction(2));
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

    private void ResetAction()
    {
        m_PlayerAnimation.ResetAnim();
    }


    #region 값 전달 함수들

    public int GetCurrSp()
    {
        return m_CurrSp;
    }

    public void SetCurrSp(int _currSp)
    {
        this.m_CurrSp = _currSp;
    }

    public int GetMaxSp()
    {
        return m_MaxSp;
    }

    public void SetMaxSp(int _maxSp)
    {
        this.m_MaxSp = _maxSp;
    }

    public bool GetIsFlipX()
    {
        return m_PlayerMove.GetBoolFlipX();
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public Animator GetAnim()
    {
        if (m_PlayerAnimation == null)
            return null;
        return m_PlayerAnimation.GetAnim();
    }

    #endregion




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
                yield return new WaitForSeconds(3f);
                ++m_CurrSp;
            }
            else if (m_CurrSp == m_MaxSp)
                break;
        }
    }
}
