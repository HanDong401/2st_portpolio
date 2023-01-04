using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    #region 플레이어 이동 관련

    [SerializeField] private float m_AccelSpeed = 0f;
    [SerializeField] private float m_DashSpeed = 0f;
    [SerializeField] private float m_MoveSpeed = 0f;
    [SerializeField] private float m_MaxMoveSpeed = 0f;
    [SerializeField] private float m_MaxDashSpeed = 0f;
    [SerializeField] private float m_DodgeRange = 0f;
    [SerializeField] private float m_DodgeSpeed;
    private Rigidbody2D m_Rigid = null;
    [SerializeField] private Animator m_Anim = null;
    private SpriteRenderer m_Sprite = null;
    private Vector2 m_MoveDir;
    private Vector2 m_CurrDir;
    private bool mbIsOnMove = false;
    private bool mbIsOnDash = false;
    private bool mbIsOnDodge = false;
    private bool mbIsFlipX = false;

    #endregion

    private void Awake()
    {
        m_Rigid = this.GetComponent<Rigidbody2D>();
        m_Sprite = this.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(CheckSpeed());
    }

    private void FixedUpdate()
    {
        m_Rigid.velocity = m_MoveDir * (m_MoveSpeed + m_DashSpeed + m_DodgeSpeed) * Time.deltaTime;
    }

    public void SetAnim(Animator _anim)
    {
        m_Anim = _anim;
    }

    public bool OnMove(Vector2 _inputDir)
    {
        if (mbIsOnDodge == false)
            m_MoveDir = _inputDir;
        
            m_CurrDir = _inputDir;
        //m_CurrDirStack.Push(_inputDir);

        SetFlipX();

        if (_inputDir != Vector2.zero)
        {
            Debug.Log("이동시작");
            mbIsOnMove = true;
            StartCoroutine(AddMoveAccel());
            if (mbIsOnDash == true)
                StartCoroutine(AddDashAccel());
        }
        else
        {
            Debug.Log("이동종료");
            mbIsOnMove = false;
            StartCoroutine(ReduceMoveAccel());
            StartCoroutine(ReduceDashAccel());
        }
        return mbIsOnMove;
    }

    public void OnDash()
    {
        if (mbIsOnDash == false)
        {
            Debug.Log("대쉬!");
            mbIsOnDash = true;
            StartCoroutine(AddDashAccel());
        }
        else
        {
            Debug.Log("대쉬 취소");
            mbIsOnDash = false;
            StartCoroutine(ReduceDashAccel());
        }
    }

    public void OnDodge()
    {
        mbIsOnDodge = true;
        m_Anim.SetTrigger("IsRoll");
        //if (m_MoveDir != Vector2.zero)
        //{
        //    Vector2 prevDir = m_MoveDir;
        //}
        if (m_MoveDir == Vector2.zero)
        {
            if (m_Sprite.flipX)
                m_MoveDir = Vector2.left;
            else
                m_MoveDir = Vector2.right;
        }

        m_DodgeSpeed = m_DodgeRange;
    }

    private void OffDodge()
    {
        Debug.Log("회피 종료");
        m_MoveDir = m_CurrDir;
        SetFlipX();
        mbIsOnDodge = false;
        m_Anim.ResetTrigger("IsRoll");
        m_DodgeSpeed = 0;
    }

    public bool GetIsDodge()
    {
        return mbIsOnDodge;
    }

    void SetFlipX()
    {
        if (m_MoveDir.x < 0)
            mbIsFlipX = true;
        else if (m_MoveDir.x > 0)
            mbIsFlipX = false;
        m_Sprite.flipX = mbIsFlipX;
    }

    public bool GetBoolFlipX()
    {
        return mbIsFlipX;
    }

    public float GetSpeed()
    {
        return m_MoveSpeed + m_DashSpeed + m_DodgeSpeed;
    }


    IEnumerator CheckSpeed()
    {
        while (true)
        {
            //Debug.Log("캐릭터 속도: " + m_MoveSpeed);
            m_Anim.SetFloat("Speed", ((m_MoveSpeed + m_DashSpeed) / (m_MaxMoveSpeed + m_MaxDashSpeed)) * 1.5f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator AddMoveAccel()
    {
        Debug.Log("가속중");
        while (mbIsOnMove == true)
        {
            m_MoveSpeed += m_AccelSpeed;
            if (m_MoveSpeed > m_MaxMoveSpeed)
            {
                m_MoveSpeed = m_MaxMoveSpeed;
                break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator AddDashAccel()
    {
        while (mbIsOnDash == true && mbIsOnMove == true)
        {
            m_DashSpeed += m_AccelSpeed;
            if (m_DashSpeed > m_MaxDashSpeed)
            {
                m_DashSpeed = m_MaxDashSpeed;
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator ReduceMoveAccel()
    {
        Debug.Log("감속중");
        while (mbIsOnMove == false)
        {
            m_MoveSpeed -= m_AccelSpeed * 2f;
            if (m_MoveSpeed < 0f)
            {
                m_MoveSpeed = 0f;
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator ReduceDashAccel()
    {
        while (mbIsOnDash == false || mbIsOnMove == false)
        {
            m_DashSpeed -= m_AccelSpeed;
            if (m_DashSpeed < 0f)
            {
                m_DashSpeed = 0f;
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
