using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster
{
    public override void SubAwake()
    {
        
    }

    public override void Attack1()
    {
        Anim.SetTrigger("IsAttack");
        StartDelay("Idle", Attack1Delay);
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

    private void BatAttack1()
    {
        Target.OnDamage(Attack1Damage);
        Target.Knockback((Vector2)transform.position, Attack1Damage);
    }
}
