using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : PassiveItem
{
    [SerializeField] int m_ArmorDefense = 0;

    protected override void PassiveEffect()
    {
        m_Player.SetDefense(m_ArmorDefense);
    }
}
