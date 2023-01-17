using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : ActiveItem
{
    [SerializeField] GameObject m_MagicPrefab = null;
    private bool mbIsCanSpell = true;

    protected override void InteractionItem()
    {
        InitMagic();
    }

    private void InitMagic()
    {
        base.InitActiveItem();
    }

    protected override void Action()
    {
        if (mbIsCanSpell == true)
        {
            StartCoroutine(SpellMagic());
        }
    }

    IEnumerator SpellMagic()
    {
        m_Anim.SetTrigger("IsOnMagic");
        mbIsCanSpell = false;

        yield return new WaitForSeconds(0.2f);

        GameObject go = Instantiate(m_MagicPrefab);
        go.transform.position = m_Player.GetPosition();
        go.SetActive(true);

        yield return new WaitForSeconds(1f);
        mbIsCanSpell = true;
    }

}
