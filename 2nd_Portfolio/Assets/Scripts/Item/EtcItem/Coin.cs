using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : EtcItem
{
    [SerializeField] private int m_CoinPoint = 1;
    private void OnEnable()
    {
        StartCoroutine(PickUpDelay());
    }

    protected override void EtcItemEffect()
    {
        Debug.Log("ÄÚÀÎ Å‰µæ ½ÇÇà");
        Player.SetCoin(m_CoinPoint);
    }

    IEnumerator PickUpDelay()
    {
        yield return new WaitForSeconds(1f);
        mbIsCanPick = true;
    }
}
