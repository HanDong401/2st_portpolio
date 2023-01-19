using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack3State : MonsterBaseState
{
    public MonsterAttack3State(Monster _monster) : base(_monster) { }

    public override void EnterState()
    {
        Debug.Log("Attack3 ¿‘¿Â!!");
        m_Monster.Attack3();
        m_Monster.Attack3CoolTime(m_Monster.Attack3Delay);
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
