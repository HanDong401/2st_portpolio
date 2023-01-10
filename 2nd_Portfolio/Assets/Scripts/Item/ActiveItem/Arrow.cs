using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D m_Rigid2D = null;
    private Vector2 m_MoveDir;
    private int m_Damage = 5;
    [SerializeField] private float m_ArrowSpeed = 0f;
    [SerializeField] private int m_ReflectCount = 3;

    private void Awake()
    {
        m_Rigid2D = this.GetComponent<Rigidbody2D>();
        m_MoveDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.right = m_MoveDir;
        ShotArrow(m_MoveDir);
    }

    private void ShotArrow(Vector2 _vector)
    {
        m_Rigid2D.AddForce(_vector * m_ArrowSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        --m_ReflectCount;
        if (m_ReflectCount <= 0)
        {
            m_Rigid2D.velocity = Vector2.zero;
            m_Rigid2D.isKinematic = true;
            Destroy(this.gameObject, 2f);
            return;
        }
        m_MoveDir = Vector2.Reflect(m_MoveDir, coll.contacts[0].normal);
        m_Rigid2D.velocity = Vector2.zero;
        m_Rigid2D.angularVelocity = 0f;
        ShotArrow(m_MoveDir);
        transform.right = m_MoveDir;

        if (coll.transform.CompareTag("Monster"))
        {
            coll.transform.GetComponent<Monster>().OnDamaged(m_Damage);
        }
    }
}
