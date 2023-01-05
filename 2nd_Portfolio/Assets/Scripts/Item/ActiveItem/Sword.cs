using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : ActiveItem
{
    [SerializeField] private Vector3 m_ControlPointVec = Vector3.zero;
    [SerializeField] private Vector3 m_SizeVec = Vector3.zero;
    [SerializeField] private int m_Damage = 10;
    private Vector3 m_PointVec = Vector3.zero;
    private Coroutine m_DelayCoroutine = null;
    private Transform m_TargetTransform = null;
    private int m_SwordAttackCount = 0;
    private Collider2D m_HitColl = null;

    protected override void InteractionItem()
    {
        InitSword();
    }

    private void InitSword()
    {
        m_TargetTransform = m_Player.GetTransform();
        StartCoroutine(SetPointVec());
        StartCoroutine(SetControlPointVec());
    }

    protected override void Action()
    {
        if (m_SwordAttackCount < 3)
        {
            Attack();
            ++m_SwordAttackCount;
            if (m_DelayCoroutine != null)
            {
                StopCoroutine(m_DelayCoroutine);
            }
            m_DelayCoroutine = StartCoroutine(SwordDealy());
        }
    }

    private void Attack()
    {
        m_Anim.ResetTrigger("IsOnSword");
        m_Anim.SetTrigger("IsOnSword");
        HitBox();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(m_PointVec, m_SizeVec);
    }

    private void HitBox()
    {
        Collider2D hit = Physics2D.OverlapBox(m_PointVec, m_SizeVec, 0f);

        if (hit != null)
        {
            if (hit.CompareTag("Monster"))
            {
                hit.GetComponent<Monster>().Damaged(m_Damage);
            }
        }
    }

    IEnumerator SwordDealy()
    {
        yield return new WaitForSeconds(1f);

        m_SwordAttackCount = 0;
    }

    IEnumerator SetPointVec()
    {
        while(true)
        {
            m_PointVec = m_TargetTransform.position + m_ControlPointVec;
            yield return null;
        }
    }

    IEnumerator SetControlPointVec()
    {
        while(true)
        {
            if (m_Player.GetIsFlipX() == true)
                m_ControlPointVec.x = -1;
            else
                m_ControlPointVec.x = 1;
            yield return null;
        }
    }
}
