using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� ���� ��带 ������Ű�� �Լ� ����
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
