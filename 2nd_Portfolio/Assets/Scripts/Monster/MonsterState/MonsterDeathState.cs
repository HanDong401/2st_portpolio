using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathState : MonsterBaseState
{
    public MonsterDeathState(Monster _monster) : base(_monster) { }
    private float m_Delay = 0f;

    public override void EnterState()
    {
        m_Monster.Anim.SetTrigger("IsDeath");
        m_Monster.Rigid2D.isKinematic = true;
        m_Monster.Rigid2D.velocity = Vector2.zero;
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
