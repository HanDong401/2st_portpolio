using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DungeonToNextFloor : MonoBehaviour, Interaction
{
    [SerializeField] private Image[] NPCImage = null;
    public delegate Player spawnEvent();
    private spawnEvent m_SpawnEvent;
    Player m_Player= null;

    private void OnEnable()
    {
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = false;
        }
    }
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
