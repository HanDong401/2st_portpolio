using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDoor : Door
{
    protected override void SubAwake()
    {
        DoorOpen();
    }

    protected override void SubExecute()
    {
        if (m_DoorEvent != null)
            m_DoorEvent("DungeonScene");
    }

}
