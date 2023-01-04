using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : Item
{
    protected virtual void PassiveEffect()
    {
        Debug.Log("패시브 효과 실행");
    }
}
