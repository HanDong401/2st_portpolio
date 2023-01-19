using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : Monster
{
    public override void SubAwake()
    {

    }

    public override void Attack1()
    {
        Anim.SetTrigger("IsAttack");
        StartCoroutine(PebbleExplosion());
    }

    public override bool SubCheckState()
    {
        return false;
    }

    public override void Attack2()
    {
        
    }

    public override void Attack3()
    {
        
    }

    public override void Ability()
    {
        
    }

    public override void Death()
    {
        Anim.SetTrigger("IsDeath");
        Collider.enabled = false;
        Destroy(this.gameObject, 1f);
    }

    IEnumerator PebbleExplosion()
    {
        yield return new WaitForSeconds(Attack1Delay);
        Collider2D coll = Physics2D.OverlapCircle(transform.position, 3f, TargetLayer);
        if (coll != null)
        {
            Target.PlayerDamage(Attack1Damage);
            Target.Knockback(transform.position, Attack1Damage);
        }
    }

    private void DestroyPebble()
    {
        Destroy(this.gameObject);
    }
}
