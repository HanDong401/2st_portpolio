using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : ActiveItem
{
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
        m_Anim.SetTrigger("IsOnBow");
    }
}
