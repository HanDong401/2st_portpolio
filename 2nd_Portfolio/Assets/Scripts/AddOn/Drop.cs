using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    Vector2 m_StartPoint;

    public void DropObject(Rigidbody2D _rigid, float _power = 1f)
    {
        m_StartPoint = transform.position;
        Vector2 dropDir = new Vector2(Random.Range(-2f, 2f), 10f).normalized;
        _rigid.AddForce(dropDir * _power, ForceMode2D.Impulse);
        StartCoroutine(StopCoroutine(_rigid));
    }

    IEnumerator StopCoroutine(Rigidbody2D _rigid)
    {
        while (true)
        {
            if (transform.position.y < m_StartPoint.y)
            {
                _rigid.gravityScale = 0f;
                _rigid.velocity = Vector2.zero;
                _rigid.isKinematic = true;
                yield break;
            }
            yield return null;
        }
    }

}
