using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem2 : Monster
{
    private GolemUI m_GolemUI = null;

    public override void SubAwake()
    {
        m_GolemUI = this.GetComponentInChildren<GolemUI>();
        StartCoroutine(SetHP());
    }

    public override void Ability()
    {
        Anim.SetTrigger("IsAbility");
        IsCanAbility = false;
    }

    public override void Attack1()
    {
        Anim.SetTrigger("IsAttack");
        IsCanAttack1 = false;
    }

    public override void Attack2()
    {
        
    }

    public override void Attack3()
    {
        
    }

    public override bool SubCheckState()
    {
        return false;
    }

    private void Golem2Attack1()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, Attack1Range, TargetLayer);
        if (coll != null)
        {
            Target.PlayerDamage(Attack1Damage);
            Target.Knockback(transform.position, Attack1Damage);
        }
        StartDelay("Idle", 1f);
    }

    private void Golem2Ability()
    {
        m_MonsterSummonEvent("Golem3", transform.position);
        Destroy(this.gameObject);
    }

    public override void Death()
    {
        ChangeState("Ability");
    }

    IEnumerator SetHP()
    {
        while (true)
        {
            m_GolemUI.InitGolemUI(m_CurrHp, m_MaxHp);
            yield return null;
        }
    }
}
