using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : ActiveItem
{
    protected override void Action()
    {
        Debug.Log("���� ����!!");
        m_Anim.SetTrigger("IsOnMagic");
    }
}
