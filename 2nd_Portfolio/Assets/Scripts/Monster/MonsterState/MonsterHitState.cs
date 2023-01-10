using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitState : MonsterBaseState
{
    public MonsterHitState(Monster _monster) : base(_monster) { }
    private float m_Delay = 0f;
    public override void EnterState()
    {
        Debug.Log("Enter Hit");
        m_Monster.Rigid2D.isKinematic = true;
        m_Monster.Rigid2D.velocity = Vector2.zero;
        m_Monster.Anim.SetTrigger("IsHit");
        m_Delay = 0f;
    }

    public override void UpdateState()
    {
        m_Delay += Time.deltaTime;
    }

    public override void ExitState()
    {
        m_Monster.Anim.ResetTrigger("IsHit");
        m_Monster.Rigid2D.isKinematic = false;
    }

    public override void CheckState()
    {
        if (m_Monster.GetCurrHp() <= 0)
        {
            m_Monster.ChangeState("Death");
            return;
        }
        if (m_Delay > 1f)
            m_Monster.ChangeState("Idle");
    }
}
