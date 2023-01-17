using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack1State : MonsterBaseState
{
    public MonsterAttack1State(Monster _monster) : base(_monster) { }
    private float m_Delay = 0f;

    public override void EnterState()
    {
        Debug.Log("Attack1 ¿‘¿Â!!");
        m_Monster.Attack1();
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        m_Monster.Anim.ResetTrigger("IsAttack");
        if (m_Monster.Collider.enabled == false)
            m_Monster.Collider.enabled = true;

    }

    public override void CheckState()
    {

    }
}
