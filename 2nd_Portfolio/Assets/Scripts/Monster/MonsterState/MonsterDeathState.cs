using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathState : MonsterBaseState
{
    public MonsterDeathState(Monster _monster) : base(_monster) { }

    public override void EnterState()
    {
        m_Monster.Anim.SetTrigger("IsDeath");
        m_Monster.Collider.enabled = false;
        GameObject.Destroy(m_Monster.gameObject, 1f);
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
