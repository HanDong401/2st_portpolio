using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack1State : MonsterBaseState
{
    public MonsterAttack1State(Monster _monster) : base(_monster) { }
    private float m_Delay = 0f;

    public override void EnterState()
    {
        m_Monster.Anim.SetTrigger("IsAttack");
        m_Monster.Target.GetComponent<Player>().OnDamage(m_Monster.Damage);
        m_Delay = 0f;
    }

    public override void UpdateState()
    {
        m_Delay += Time.deltaTime;
    }

    public override void ExitState()
    {
        m_Monster.Anim.ResetTrigger("IsAttack");
    }

    public override void CheckState()
    {
        if (m_Delay > 1f)
            m_Monster.ChangeState("Idle");
    }
}
