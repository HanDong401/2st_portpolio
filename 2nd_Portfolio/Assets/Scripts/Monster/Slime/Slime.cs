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
        Anim.SetTrigger("IsAttack");
        IsCanAttack1 = false;
        StartCoroutine(Attack1Coroutine());
        
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

    public override void Death()
    {
        Anim.SetTrigger("IsDeath");
        Collider.enabled = false;
        GameObject.Destroy(this.gameObject, 1f);
    }

    private void SlimeAttack1()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, 1f, TargetLayer);
        if (coll != null)
        {
            Target.PlayerDamage(Attack1Damage);
            Target.Knockback(transform.position, Attack1Damage);
        }
        StartDelay("Idle", 1f);
    }

    IEnumerator SlimeAbility()
    {
        Anim.SetTrigger("IsAbility");
        Monster monster = m_MonsterSummonEvent("MiniSlime", transform.position);
        monster.m_SummonRoom.m_MonsterList.Add(monster);
        monster.InitMonster();
        Rigidbody2D rigid = monster.GetComponent<Rigidbody2D>();
        monster.GetComponent<Drop>().DropObject(rigid, 8f);
        Debug.Log(this.gameObject.name + "어빌리티 발동");
        yield return new WaitForSeconds(1f);
        ChangeState("Idle");
    }

    IEnumerator CheckAbility()
    {
        ChangeState("Ability");
        yield return new WaitForSeconds(3f);
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
