using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Monster
{
    public override void SubAwake()
    {

    }

    public override void Ability()
    {
        Anim.SetTrigger("IsAbility");
        Rat[] rats = GameObject.FindObjectsOfType<Rat>();
        foreach(Rat rat in rats)
        {
            rat.CallRats(99f);
        }
        IsCanAbility = false;
        StartDelay("Idle", 2f);
    }

    public override void Attack1()
    {
        Anim.SetTrigger("IsAttack");
        IsCanAttack1 = false;
        Collider2D coll = Physics2D.OverlapCircle(transform.position, Attack1Range, TargetLayer);
        if (coll != null)
        {
            Target.PlayerDamage(Attack1Damage);
            Target.Knockback(transform.position, Attack1Damage);
        }
        StartDelay("Idle", 1f);
    }

    public override void Attack2()
    {
        
    }

    public override void Attack3()
    {
        
    }

    public override bool SubCheckState()
    {
        if (mbIsCanAbility.Equals(true) && GetCurrHp() <= (GetMaxHp() * 0.4f))
        {
            ChangeState("Ability");
            return true;
        }
        return false;
    }

    public override void Death()
    {
        Anim.SetTrigger("IsDeath");
        Collider.enabled = false;
        Destroy(this.gameObject, 1f);
    }

    public void CallRats(float _range)
    {
        m_DetectRange = _range;
    }
}
