using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(Monster _monster) : base(_monster) { }

    public override void EnterState()
    {
        Debug.Log("Idel Enter");
        m_Monster.Anim.SetTrigger("IsIdle");
    }

    public override void UpdateState()
    {
        Collider2D target = Physics2D.OverlapCircle(m_Monster.GetPosition(), m_Monster.DetectRange, m_Monster.TargetLayer);
        if (target != null)
        {
            m_Monster.Target = target.GetComponent<Player>().GetTransform();
        }
    }

    public override void ExitState()
    {
        m_Monster.Anim.ResetTrigger("IsIdle");
    }

    public override void CheckState()
    {
        if (m_Monster.Target != null && Vector2.Distance(m_Monster.GetPosition(), m_Monster.Target.position) < m_Monster.DetectRange)
            m_Monster.ChangeState("Run");
    }
}
