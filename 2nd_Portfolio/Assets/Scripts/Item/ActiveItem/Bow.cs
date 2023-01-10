using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : ActiveItem
{
    [SerializeField] GameObject m_ArrowPrefab = null;
    private bool mbIsCanShot = true;

    protected override void InteractionItem()
    {
        InitBow();
    }

    private void InitBow()
    {
        base.InitActiveItem();
    }

    protected override void Action()
    {
        if (mbIsCanShot == true)
        {
            StartCoroutine(ShotBow());
        }
    }

    IEnumerator ShotBow()
    {
        m_Anim.SetTrigger("IsOnBow");
        mbIsCanShot = false;

        yield return new WaitForSeconds(0.5f);

        GameObject go = Instantiate(m_ArrowPrefab);
        go.transform.position = m_Player.GetTransform().position;
        go.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        mbIsCanShot = true;
    }
}
