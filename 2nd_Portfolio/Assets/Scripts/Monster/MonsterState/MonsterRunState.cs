using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterRunState : MonsterBaseState
{
    public MonsterRunState(Monster _monster) : base(_monster) { }
    private int m_ListCount = 0;
    private float m_Count = 1;
    private List<Node> m_NodeList;
    private bool mbIsAStar = false;
    private bool mbIsLookPlayer = false;

    Vector2 m_NextPos;
    

    public override void EnterState()
    {
        Debug.Log("Run 입장!!");
        m_Monster.Anim.SetBool("IsRun", true);
   }

    public override void UpdateState()
    {
        m_CurrPos = m_Monster.GetPosition();
        m_CurrTargetPos = m_Monster.GetTargetPosition();
        CheckFlipX();
        // 일정 시간마다 작동하는 방식
        m_Count += Time.deltaTime;
        if (m_Count > 1.0f)
        {
            RaycastHit2D hit = Physics2D.Raycast(m_CurrPos, m_CurrTargetPos - m_CurrPos, m_Monster.DetectRange, ~m_Monster.IgnoreLayer);
            if (hit.collider == null)
            {
                m_Monster.ChangeState("Idle");
            }
            if (hit.transform.CompareTag("Player"))
            {
                m_NextPos = m_CurrTargetPos;
                mbIsAStar = false;
                mbIsLookPlayer = true;
            }
            else
            {
                m_NodeList = m_Monster.m_MonsterEvent(m_CurrPos, m_CurrTargetPos);
                m_ListCount = 1;
                UpdateNextPos();
                mbIsAStar = true;
                mbIsLookPlayer = false;
            }
            m_Count = 0f;
        }

        // 다음 목적지까지의 거리비교후 다음 목정지 재설정
        if (mbIsAStar.Equals(true) && Vector2.Distance(m_CurrPos, m_NextPos) < 0.1f && m_ListCount < m_NodeList.Count)
        {
            m_ListCount++;
            UpdateNextPos();
        }
        Debug.DrawRay(m_CurrPos, m_NextPos - m_CurrPos);
        m_Monster.transform.Translate((m_NextPos - m_CurrPos).normalized * Time.deltaTime * m_Monster.MoveSpeed);
    }

    public override void ExitState()
    {
        m_Monster.Anim.SetBool("IsRun", false);
    }

    public override void CheckState()
    {
        Vector2 dir = m_CurrTargetPos - m_CurrPos;
        float sqr = dir.sqrMagnitude;
        if (m_Monster.SubCheckState().Equals(true)) return;

        if (m_Monster.IsCanAttack1.Equals(true) && mbIsLookPlayer.Equals(true) && sqr < m_Monster.Attack1Range * m_Monster.Attack1Range)
            m_Monster.ChangeState("Attack1");
        else if (m_Monster.IsCanAttack2.Equals(true) && mbIsLookPlayer.Equals(true) && sqr > m_Monster.Attack1Range * m_Monster.Attack1Range && sqr < m_Monster.Attack2Range * m_Monster.Attack2Range)
            m_Monster.ChangeState("Attack2");
        else if (m_Monster.IsCanAttack3.Equals(true) && mbIsLookPlayer.Equals(true) && sqr > m_Monster.Attack2Range * m_Monster.Attack2Range && sqr < m_Monster.Attack3Range * m_Monster.Attack3Range)
            m_Monster.ChangeState("Attack3");
        else if (sqr > m_Monster.DetectRange * m_Monster.DetectRange)
            m_Monster.ChangeState("Idle");
    }

    private void CheckFlipX()
    {
        Vector2 targetPos = (m_CurrTargetPos - m_CurrPos).normalized;

        if (targetPos.x < 0)
            m_Monster.Renderer.flipX = true;
        else if (targetPos.x > 0)
            m_Monster.Renderer.flipX = false;
    }

    private void UpdateNextPos()
    {
        if (m_ListCount < m_NodeList.Count)
        {
            m_NextPos = new Vector2(m_NodeList[m_ListCount].x, m_NodeList[m_ListCount].y);
        }
        else
        {
            Debug.Log("노드리스트 오류");
            m_Monster.ChangeState("Idle");
        }
    }
}
