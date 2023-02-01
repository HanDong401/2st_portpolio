using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : EtcItem
{
    [SerializeField] private int HealPoint = 10;

    private void OnEnable()
    {
        StartCoroutine(CheckHp());
    }

    protected override void EtcItemEffect()
    {
        Debug.Log("체력회복 실행");
        Player.SetCurrHp(HealPoint);
    }

    IEnumerator CheckHp()
    {
        while(true)
        {
            if (Player != null)
            {
                if (Player.GetCurrHp() < Player.GetMaxHp())
                {
                    IsCanPick = true;
                    yield break;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
