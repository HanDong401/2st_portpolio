using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem1 : Monster
{
    public override void SubAwake()
    {

    }

    public override void Ability()
    {
        // ü���� �ٱ����� Ȥ�� �׾����� �ߵ��ϰ�
        // �ߵ��Ҷ� ��� Ÿ�ֿ̹� ���ӿ�����Ʈ�� �������� ����
    }

    public override void Attack1()
    {

    }

    public override void Attack2()
    {
        StartCoroutine(Attack2Coroutine());
    }

    public override void Attack3()
    {
        Anim.SetTrigger("IsAttack3");
        StartDelay("Idle", Attack3Delay);
    }

    public override bool SubCheckState()
    {
        return false;
    }

    private void GolemAttack3()
    {
        Target.PlayerDamage(Attack3Damage);
        Target.Knockback(transform.position, Attack3Damage);
    }

    private void GolemAbility()
    {
        m_MonsterSummonEvent("Golem2", transform.position);
    }

    IEnumerator Attack2Coroutine()
    {
        AttackRange attackRange = Instantiate(AttackRangePrefab);
        attackRange.transform.position = transform.position;
        while (true)
        {
            if (attackRange.EffectRange(Attack2Range, Attack2Delay * Time.deltaTime).Equals(true))
            {
                Destroy(attackRange.gameObject);
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
        StartDelay("Idle", Attack2Delay);
    }
}
