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
        Rat[] rats = GameObject.FindObjectsOfType<Rat>();
    }

    public override void Attack1()
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
        if (mbIsCanAbility.Equals(true) && GetCurrHp() <= (GetMaxHp() * 0.4f))
        {
            StartCoroutine(CheckAbility());
            return true;
        }
        return false;
    }

    IEnumerator CheckAbility()
    {
        ChangeState("Ability");
        yield return new WaitForSeconds(AbilityDelay);
        mbIsCanAbility = false;
    }
}
