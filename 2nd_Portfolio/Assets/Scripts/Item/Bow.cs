using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : ActiveItem
{
    protected override void Action()
    {
        Debug.Log("È° ½ÇÇà!!");
        m_Anim.SetTrigger("IsOnBow");
    }
}
