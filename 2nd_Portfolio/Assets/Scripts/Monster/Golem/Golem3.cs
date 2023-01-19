using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem3 : Monster
{
    [Tooltip ("발사체 프리팹")]
    [SerializeField] private Blast m_BlastPrefab = null;

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
        mbIsCanAbility = false;
    }

    public override void Attack1()
    {
        if (m_Coroutine != null)
        {
            StopCoroutine(m_Coroutine);
            if (m_AttackRange != null)
                Destroy(m_AttackRange.gameObject);
        }
        m_Coroutine = StartCoroutine(Attack1Coroutine());
        IsCanAttack1 = false;
    }

    public override void Attack2()
    {
        Anim.SetTrigger("IsAttack2");
        IsCanAttack2 = false;
    }

    public override void Attack3()
    {
        Anim.SetTrigger("IsAttack3");
        IsCanAttack3 = false;
    }

    public override bool SubCheckState()
    {
        if (IsCanAbility.Equals(true))
        {
            StartDelay("Ability", 1f);
            IsCanAbility = false;
        }
        return false;
    }

    public override void Death()
    {
        Anim.SetTrigger("IsDeath");
        if (m_Coroutine != null)
        {
            StopCoroutine(m_Coroutine);
            if (m_AttackRange != null)
                Destroy(m_AttackRange.gameObject);
        }
        Collider.enabled = false;
    }

    private void Golem3Death()
    {
        Destroy(this.gameObject, 1f);
    }

    private void Golem3Attack2()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, Attack2Range, TargetLayer);
        if (coll != null)
        {
            Target.PlayerDamage(Attack2Damage);
            Target.Knockback(transform.position, Attack2Damage);
        }
        StartDelay("Idle", 1f);
    }

    private void Golem3Attack3()
    {
        Blast blast = Instantiate(m_BlastPrefab);
        blast.transform.position = transform.position;
        blast.ShotBlast(Target, Attack3Damage);
        StartDelay("Idle", 1f);
    }

    private void Golem3Ability()
    {
        for (int i = 0; i < 4; ++i)
        {
            Vector2 spawnPoint = (Vector2)transform.position + (Random.insideUnitCircle * 3f);
            m_MonsterSummonEvent("Pebble", spawnPoint);
        }
        StartDelay("Idle", 2f);
    }

    IEnumerator Attack1Coroutine()
    {
        m_AttackRange = Instantiate(AttackRangePrefab);
        m_AttackRange.transform.position = transform.position;
        while (true)
        {
            if (m_AttackRange.EffectRange(Attack1Range, Attack1Delay * Time.deltaTime).Equals(true))
            {
                Anim.SetTrigger("IsAttack");
                Collider2D target = Physics2D.OverlapCircle(transform.position, Attack1Range, TargetLayer);
                if (target != null)
                {
                    Target.PlayerDamage(Attack1Damage);
                    Target.Knockback((Vector2)transform.position, Attack1Damage);
                }
                break;
            }
            yield return null;
        }
        StartDelay("Idle", 1f);
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
