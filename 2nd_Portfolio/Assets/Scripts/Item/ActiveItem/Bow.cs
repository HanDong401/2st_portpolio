using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : ActiveItem
{
    private void OnEnable()
    {
        InitBow();
    }

    private void InitBow()
    {
        base.InitActiveItem();
    }

    protected override void Action()
    {
        Debug.Log("Ȱ ����!!");
        m_Anim.SetTrigger("IsOnBow");
    }
}
