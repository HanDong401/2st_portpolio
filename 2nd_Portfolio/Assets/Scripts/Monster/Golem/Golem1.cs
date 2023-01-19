using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem1 : Monster
{
    private GolemUI m_GolemUI = null;
    private AttackRange m_AttackRange = null;
    private Coroutine m_Coroutine = null;

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

    }

    public override void Attack2()
    {
        if (m_Coroutine != null)
        {
            StopCoroutine(m_Coroutine);
            if (m_AttackRange != null)
                Destroy(m_AttackRange.gameObject);
        }
        m_Coroutine = StartCoroutine(Attack2Coroutine());
        IsCanAttack2 = false;
    }

    public override void Attack3()
    {
        Anim.SetTrigger("IsAttack3");
        IsCanAttack3 = false;
    }

    public override bool SubCheckState()
    {
        return false;
    }

    public override void Death()
    {
        ChangeState("Ability");
        if (m_Coroutine != null)
        {
            StopCoroutine(m_Coroutine);
            if (m_AttackRange != null)
                Destroy(m_AttackRange.gameObject);
        }
    }

    private void GolemAttack3()
    {
        Vector2 targetPos = Target.GetPosition();
        Vector2 targetDir = (targetPos - (Vector2)transform.position).normalized;
        if (Renderer.flipX.Equals(true))
        {
            if (targetDir.x > 0) return;
        }
        else
        {
            if (targetDir.x < 0) return;
        }
        Target.PlayerDamage(Attack3Damage);
        Target.Knockback(transform.position, Attack3Damage);
        StartDelay("Idle", 1f);
        Anim.ResetTrigger("IsAttack3");
    }

    private void Golem1Ability()
    {
        m_MonsterSummonEvent("Golem2", transform.position);
        Destroy(this.gameObject);
    }

    IEnumerator Attack2Coroutine()
    {
        m_AttackRange = Instantiate(AttackRangePrefab);
        m_AttackRange.transform.position = transform.position;
        while (true)
        {
            if (m_AttackRange.EffectRange(Attack2Range, Attack2Delay * Time.deltaTime).Equals(true))
            {
                Anim.SetTrigger("IsAttack2");
                Collider2D target = Physics2D.OverlapCircle(transform.position, Attack2Range, TargetLayer);
                if (target != null)
                {
                    Target.PlayerDamage(Attack2Damage);
                    Target.Knockback((Vector2)transform.position, Attack2Damage);
                }
                break;
            }
            yield return null;
        }
        StartDelay("Idle", 1f);
    }

    IEnumerator SetHP()
    {
        while(true)
        {
            m_GolemUI.InitGolemUI(m_CurrHp, m_MaxHp);
            yield return null;
        }
    }
}
