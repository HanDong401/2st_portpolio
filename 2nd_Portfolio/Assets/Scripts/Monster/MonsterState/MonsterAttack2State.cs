using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack2State : MonsterBaseState
{
    public MonsterAttack2State(Monster _monster) : base(_monster) { }

    public override void EnterState()
    {
        Debug.Log("Attack2 ¿‘¿Â!!");
        m_Monster.Attack2();
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        m_Monster.Anim.ResetTrigger("IsAttack2");
        if (m_Monster.Collider.enabled == false)
            m_Monster.Collider.enabled = true;
    }

    public override void CheckState()
    {

    }
}
