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
            if (hit.transform.CompareTag("Player"))
            {
                m_NextPos = m_CurrTargetPos;
                mbIsAStar = false;
            }
            else
            {
                m_NodeList = m_Monster.m_MonsterEvent(m_CurrPos, m_CurrTargetPos);
                m_ListCount = 0;
                mbIsAStar = true;
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

        if (sqr > m_Monster.DetectRange * m_Monster.DetectRange)
            m_Monster.ChangeState("Idle");
        else if (sqr > m_Monster.Attack2Range * m_Monster.Attack2Range && sqr < m_Monster.Attack3Range * m_Monster.Attack3Range)
            m_Monster.ChangeState("Attack3");
        else if (sqr > m_Monster.Attack1Range * m_Monster.Attack1Range && sqr < m_Monster.Attack2Range * m_Monster.Attack2Range)
            m_Monster.ChangeState("Attack2");
        else if (sqr < m_Monster.Attack1Range * m_Monster.Attack1Range)
            m_Monster.ChangeState("Attack1");
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
            m_NextPos = new Vector2(m_NodeList[m_ListCount].x, m_NodeList[m_ListCount].y);
        else
        {
            Debug.Log("리스트 카운트 오류발생");
            return;
        }
    }
}
