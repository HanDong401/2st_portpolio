using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSkill : MonoBehaviour
{
    private Animator m_SkillAnim = null;
    private Rigidbody2D m_Rigid2D = null;
    private Vector2 m_MoveDir;
    private int m_Damage = 20;
    [SerializeField] private float m_MagicSpeed = 0f;
    [SerializeField] private float m_DamageRange = 0f;
    [SerializeField] private LayerMask m_MonsterLayer;

    private void Awake()
    {
        m_SkillAnim = GetComponent<Animator>();
        m_Rigid2D = this.GetComponent<Rigidbody2D>();
        m_MoveDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.right = m_MoveDir;
        SpellMagic(m_MoveDir);
    }

    private void SpellMagic(Vector2 _vector)
    {
        m_Rigid2D.AddForce(_vector * m_MagicSpeed, ForceMode2D.Impulse);
    }

    private void FindRange()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, m_DamageRange, m_MonsterLayer);

        if (monsters.Length > 0)
        {
            foreach(var monster in monsters)
            {
                monster.GetComponent<Monster>().OnDamaged(m_Damage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        transform.rotation = Quaternion.identity;
        m_Rigid2D.velocity = Vector2.zero;
        m_Rigid2D.isKinematic = true;
        m_SkillAnim.SetBool("IsExplosion", true);
        FindRange();
    }

    private void DestroySkill()
    {
        Destroy(this.gameObject);
    }

}
