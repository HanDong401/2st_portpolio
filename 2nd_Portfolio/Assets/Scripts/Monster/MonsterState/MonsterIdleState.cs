using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(Monster _monster) : base(_monster) { }
    public override void EnterState()
    {
        Debug.Log("Idle ¿‘¿Â!!");
        m_CurrPos = m_Monster.GetPosition();
    }

    public override void UpdateState()
    {
        if (m_Monster.Target == null)
        {
            Collider2D target = Physics2D.OverlapCircle(m_CurrPos, m_Monster.DetectRange, m_Monster.TargetLayer);
            if (target != null)
            {
                m_Monster.Target = target.GetComponent<Player>();
            }
        }
        else
        {
            m_CurrTargetPos = m_Monster.GetTargetPosition();
        }
        
    }

    public override void ExitState()
    {

    }

    public override void CheckState()
    {
        if (m_Monster.SubCheckState().Equals(true)) return;
        if (m_Monster.Target == null) return;
        Vector2 dir = m_CurrTargetPos - m_CurrPos;
        float sqr = dir.sqrMagnitude;
        if (sqr < m_Monster.DetectRange * m_Monster.DetectRange)
            m_Monster.ChangeState("Run");
    }
}
