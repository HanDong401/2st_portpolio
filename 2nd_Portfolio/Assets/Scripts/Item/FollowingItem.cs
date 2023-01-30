using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingItem : MonoBehaviour
{
    [SerializeField] float m_Speed = 0.04f;
    Coroutine m_Coroutine;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
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
}
