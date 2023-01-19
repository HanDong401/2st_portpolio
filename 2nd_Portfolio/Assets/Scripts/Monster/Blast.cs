using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blast : MonoBehaviour
{
    private Rigidbody2D m_Rigid = null;
    private Player m_Player = null;
    private int m_BlastDamage = 0;
    [SerializeField] private ParticleSystem m_BlastParticle = null;
    [SerializeField] private float m_MoveSpeed = 0f;

    private void Awake()
    {
        m_Rigid = this.GetComponent<Rigidbody2D>();
    }

    public void ShotBlast(Player _target, int _damage)
    {
        m_Player = _target;
        m_BlastDamage = _damage;
        Vector2 moveDir = (m_Player.GetPosition() - (Vector2)transform.position).normalized;
        transform.right = moveDir;
        m_Rigid.AddForce(moveDir * m_MoveSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        ParticleSystem particle = Instantiate(m_BlastParticle);
        particle.transform.position = coll.GetContact(0).point;
        if (coll.gameObject.CompareTag("Player"))
        {
            // 피격판정
            m_Player.PlayerDamage(m_BlastDamage);
            m_Player.Knockback(transform.position, m_BlastDamage);
            Debug.Log("Blast 피격");
        }
        Destroy(this.gameObject);
    }
}
