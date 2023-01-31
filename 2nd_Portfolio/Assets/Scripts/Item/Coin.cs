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
            // 플레이어의 소지 골드를 증가시키는 함수 실행
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
