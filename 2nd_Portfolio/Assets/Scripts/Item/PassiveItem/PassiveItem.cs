using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : Item
{
    protected override void Interaction()
    {
        PassiveEffect();
    }

    protected virtual void PassiveEffect()
    {

    }
}
