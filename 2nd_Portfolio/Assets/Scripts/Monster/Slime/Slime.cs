using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : Monster
{
    SlimeBody m_Body = null;

    public override void SubAwake()
    {
        m_Body = GetComponentInChildren<SlimeBody>();
        m_Body.AddSlimeBodyEvent(SlimeAttack1);
    }

    public override void Attack1()
    {
        m_Body = GetComponent<SlimeBody>();
        Anim.SetTrigger("IsAttack");
        StartCoroutine(Attack1Coroutine());
        StartDelay("Idle", Attack1Delay);
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

    public override void Ability()
    {
        StartCoroutine(SlimeAbility());
    }


    public override void Attack2()
    {
        
    }

    public override void Attack3()
    {
        
    }

    private void SlimeAttack1()
    {
        Target.PlayerDamage(Attack1Damage);
        Target.Knockback(transform.position, Attack1Damage);
    }

    IEnumerator SlimeAbility()
    {
        Monster monster = m_MonsterSummonEvent("MiniSlime", transform.position);
        monster.InitMonster();
        Rigidbody2D rigid = monster.GetComponent<Rigidbody2D>();
        monster.GetComponent<Drop>().DropObject(rigid, 8f);
        Debug.Log(this.gameObject.name + "어빌리티 발동");
        yield return null;
    }

    IEnumerator CheckAbility()
    {
        ChangeState("Ability");
        yield return new WaitForSeconds(AbilityDelay);
        mbIsCanAbility = false;
    }

    IEnumerator Attack1Coroutine()
    {
        float count = 0f;
        while (count < 1f)
        {
            transform.position = Vector2.Lerp(transform.position, Target.GetPosition(), Time.deltaTime);
            count += Time.deltaTime;
            yield return null;
        }
    }
}
