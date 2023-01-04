using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : ActiveItem
{
    [SerializeField] private Vector3 m_PointVec = Vector3.zero;
    [SerializeField] private Vector3 m_SizeVec = Vector3.zero;
    [SerializeField] private int m_Damage = 10;
    private Coroutine m_DelayCoroutine = null;
    private Transform m_TargetTransform = null;
    private int m_SwordAttackCount = 0;

    private void OnEnable()
    {
        InitSword();
    }

    private void InitSword()
    {
        base.InitActiveItem();
        m_TargetTransform = m_Player.GetTransform();
        m_SizeVec = new Vector2(0.3f, 0.5f);
    }


    private void Update()
    {
        if (m_Player.GetIsFlipX() == true)
        {
            m_PointVec = m_TargetTransform.position + new Vector3(-0.4f, -0.15f);
        }
        else
        {
            m_PointVec = m_TargetTransform.position + new Vector3(0.4f, -0.15f);
        }
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
            Debug.Log("충돌판정 있음");
            Debug.Log(hit.name);
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
}
