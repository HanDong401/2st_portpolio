using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private float m_Speed = 0.04f;
    [SerializeField] private float m_DropPower = 0f;
    private Coroutine m_Coroutine;
    private Rigidbody2D m_Rigid = null;
    private Vector2 m_StartPoint;
    private bool m_IsCanFollowing = false;

    private void Awake()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
    }

    public void SetFollowing(bool _bool)
    {
        m_IsCanFollowing = _bool;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (m_IsCanFollowing.Equals(true) && coll.CompareTag("Player"))
        {
            if (m_Coroutine == null)
                m_Coroutine = StartCoroutine(UpdateCoroutine(coll));
        }
    }

    IEnumerator UpdateCoroutine(Collider2D coll)
    {
        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, coll.transform.position, m_Speed);
            yield return null;
        }
    }

    public void DropObject()
    {
        m_StartPoint = transform.position;
        float yPos = Random.Range(m_StartPoint.y - 1f, m_StartPoint.y + 1f);
        Vector2 dropDir = new Vector2(Random.Range(-2f, 2f), 10f).normalized;
        m_Rigid.AddForce(dropDir * m_DropPower, ForceMode2D.Impulse);
        StartCoroutine(StopCoroutine(yPos));
    }

    IEnumerator StopCoroutine(float _yPos)
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (transform.position.y < _yPos)
            {
                m_Rigid.gravityScale = 0f;
                m_Rigid.velocity = Vector2.zero;
                m_Rigid.isKinematic = true;
                yield break;
            }
            yield return null;
        }
    }
}
