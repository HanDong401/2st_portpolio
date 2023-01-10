using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterRunState : MonsterBaseState
{
    public MonsterRunState(Monster _monster) : base(_monster) { }
    private int m_ListCount = 0;
    private List<Node> m_NodeList;

    Vector2 m_CurrPos, m_NextPos;

    public override void EnterState()
    {
        Debug.Log("Enter Run");
        m_Monster.Anim.SetBool("IsRun", true);
        if (m_Monster.m_MonsterEvent != null)
            m_NodeList = m_Monster.m_MonsterEvent(m_Monster.GetPosition(), (Vector2)m_Monster.Target.position);
        Debug.Log(m_NodeList.Count);
        m_ListCount = 0;
        UpdateNextPos();
    }

    public override void UpdateState()
    {
        Vector2 targetPos = (m_Monster.Target.position - m_Monster.transform.position).normalized;

        if (targetPos.x < 0)
            m_Monster.Renderer.flipX = true;
        else if (targetPos.x > 0)
            m_Monster.Renderer.flipX = false;

        //m_Monster.transform.Translate( targetPos * m_Monster.MoveSpeed);
        //Vector2 nextPos = new Vector2(m_Monster.PathList[m_ListCount].x, m_Monster.PathList[m_ListCount].y);
        //m_Monster.Rigid2D.MovePosition(nextPos * Time.deltaTime);
        //if (m_ListCount < m_Monster.PathList.Count && Vector2.Distance(m_Monster.GetPosition(), nextPos) < Mathf.Epsilon)
        //    ++m_ListCount;
        if (Vector2.Distance(m_Monster.GetPosition(), m_NextPos) < 0.1)
        {
            ++m_ListCount;
            UpdateNextPos();
        }
        m_Monster.transform.Translate(m_NextPos * Time.deltaTime);
    }

    public override void ExitState()
    {
        m_Monster.Anim.SetBool("IsRun", false);
        Debug.Log("ExitRun");
    }

    public override void CheckState()
    {
        float dis = Vector2.Distance(m_Monster.GetPosition(), m_Monster.Target.position);

        if (dis > m_Monster.DetectRange)
            m_Monster.ChangeState("Idle");
        else if (dis < m_Monster.Attack3Range)
            m_Monster.ChangeState("Attack3");
        else if (dis < m_Monster.Attack2Range)
            m_Monster.ChangeState("Attack2");
        else if (dis < m_Monster.Attack1Range)
            m_Monster.ChangeState("Attack1");
    }

    private void UpdateNextPos()
    {
        if (m_ListCount < m_NodeList.Count)
            m_NextPos = new Vector2(m_NodeList[m_ListCount].x, m_NodeList[m_ListCount].y);
        else
            return;
    }
}
