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
        m_Monster.Attack2CoolTime(m_Monster.Attack2Delay);
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        if (m_Monster.Collider.enabled == false)
            m_Monster.Collider.enabled = true;
    }

    public override void CheckState()
    {

    }
}
