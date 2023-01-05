using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : ActiveItem
{
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
        m_Anim.SetTrigger("IsOnMagic");
    }
}
