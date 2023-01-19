using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonToNextFloor : MonoBehaviour, Interaction
{
    public delegate Player spawnEvent();
    private spawnEvent m_SpawnEvent;
    Player m_Player= null;

    public void AddSpawnEvent(spawnEvent _callback)
    {
        m_SpawnEvent = _callback;
    }

    [SerializeField] MapGenerateManager mapGenerateManager;
    public void InteractionExecute()
    {
        m_Player = m_SpawnEvent?.Invoke();
        //m_Player.SetTransfrom(transform.position);
        mapGenerateManager.DungeonGenerate();
    }
}
