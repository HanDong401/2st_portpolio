using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : ActiveItem
{
    private void OnEnable()
    {
        InitMagic();
    }

    private void InitMagic()
    {
        base.InitActiveItem();
    }

    protected override void Action()
    {
        Debug.Log("마법 실행!!");
        m_Anim.SetTrigger("IsOnMagic");
    }
}
