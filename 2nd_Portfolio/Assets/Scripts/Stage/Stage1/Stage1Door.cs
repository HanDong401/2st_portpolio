using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Door : Door
{
    protected override void SubAwake()
    {

    }

    protected override void SubExecute()
    {
        if (m_DoorEvent != null)
            m_DoorEvent("Stage2");
    }
}
