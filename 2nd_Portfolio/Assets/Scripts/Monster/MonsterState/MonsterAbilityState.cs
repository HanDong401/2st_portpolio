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
        m_Monster.Anim.SetTrigger("IsAbility");
        m_Monster.Ability();
        m_Delay = 0f;
    }

    public override void UpdateState()
    {
        m_Delay += Time.deltaTime;
    }

    public override void ExitState()
    {
        //m_Monster.Anim.ResetTrigger("IsAbility");
    }

    public override void CheckState()
    {
        if (m_Delay > 1f)
            m_Monster.ChangeState("Idle");
    }
}
