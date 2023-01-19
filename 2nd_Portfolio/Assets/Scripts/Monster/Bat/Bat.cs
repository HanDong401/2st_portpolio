using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster
{
    private Coroutine delay = null;
    public override void SubAwake()
    {
        
    }

    public override void Attack1()
    {
        Anim.SetTrigger("IsAttack");
        IsCanAttack1 = false;
    }

    public override void Ability()
    {
        
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

    public override void Death()
    {
        Anim.SetTrigger("IsDeath");
        Collider.enabled = false;
        GameObject.Destroy(this.gameObject, 1f);
    }

    private void BatAttack1()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, Attack1Range, TargetLayer);
        if (coll != null)
        {
            Target.OnDamage(Attack1Damage);
            Target.Knockback((Vector2)transform.position, Attack1Damage);
        }
        StartDelay("Idle", 1f);
    }
}
