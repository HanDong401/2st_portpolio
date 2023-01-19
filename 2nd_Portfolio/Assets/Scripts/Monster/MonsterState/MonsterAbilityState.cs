using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbilityState : MonsterBaseState
{
    public MonsterAbilityState(Monster _monster) : base(_monster) { }
    float m_Delay;
    public override void EnterState()
    {
        Debug.Log("Ability Enter");
        m_Monster.Ability();
        m_Monster.AbilityCoolTime(m_Monster.AbilityDelay);
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    public override void CheckState()
    {

    }
}
