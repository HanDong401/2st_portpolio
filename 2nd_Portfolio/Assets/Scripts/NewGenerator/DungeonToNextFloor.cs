using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DungeonToNextFloor : Door
{
    [SerializeField] MapGenerateManager mapGenerateManager = null;

    protected override void SubAwake()
    {
        if (mapGenerateManager == null)
            mapGenerateManager = GameObject.FindObjectOfType<MapGenerateManager>();
    }

    protected override void SubExecute()
    {
        mapGenerateManager.DungeonGenerate();
    }
}
