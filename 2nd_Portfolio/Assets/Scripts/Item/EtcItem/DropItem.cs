using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private float m_Speed = 0.04f;
    [SerializeField] private float m_DropPower = 0f;
    private EtcItem m_EtcItem = null;
    private Coroutine m_Coroutine = null;
    private Rigidbody2D m_Rigid = null;
    private Vector2 m_StartPoint;

    private void Awake()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        m_EtcItem = GetComponentInChildren<EtcItem>();
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (m_EtcItem.Player == null)
                m_EtcItem.Player = coll.GetComponent<Player>();
            if (m_EtcItem.IsCanPick.Equals(true))
            {
                if (m_Coroutine == null)
                    m_Coroutine = StartCoroutine(UpdateCoroutine(coll));
            }
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
        m_Rigid.gravityScale = 1f;
        m_StartPoint = transform.position;
        float yPos = Random.Range(m_StartPoint.y - 1f, m_StartPoint.y + 1f);
        Vector2 dropDir = new Vector2(Random.Range(-2f, 2f), 10f).normalized;
        m_Rigid.AddForce(dropDir * m_DropPower, ForceMode2D.Impulse);
        StartCoroutine(StopCoroutine(yPos));
    }

    public void SetPosition(Vector2 _pos)
    {
        this.transform.position = _pos;
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
