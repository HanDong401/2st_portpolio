using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : ActiveItem
{
    protected override void Action()
    {
        Debug.Log("°Ë ¾×¼Ç !!");
        m_Anim.SetTrigger("IsOnSword");
    }
}
