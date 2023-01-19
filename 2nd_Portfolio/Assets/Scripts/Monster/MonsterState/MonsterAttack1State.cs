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
        m_Monster.Attack1CoolTime(m_Monster.Attack1Delay);
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
