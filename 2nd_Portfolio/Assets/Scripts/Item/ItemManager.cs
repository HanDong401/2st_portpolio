using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> m_Items;
    [SerializeField] private ActiveItem[] m_ActiveItem;
    [SerializeField] private PassiveItem[] m_PassiveItem;
    [SerializeField] Chest[] m_Chests = null;
    [SerializeField] GameObject m_ChestPrefab = null;
    private void Awake()
    {
        m_Items = GetComponentsInChildren<Item>().ToList();
        foreach(var item in m_Items)
        {
            item.SetPosition(this.transform.position);
            item.SetItemActive(false);
        }
        m_Chests = GetComponentsInChildren<Chest>();
        foreach(var chest in m_Chests)
        {
            chest.SetChestEvent(RandomPopItem);
        }
    }

    // �����۸���� �����ü� �ִ� ����� �������� �̱�
    public Item RandomPopItem()
    {
        if (m_Items.Count == 0)
            return null;

        int index = 0;

        while(index < m_Items.Count)
        {
            if (m_Items[index].mbIsPickUp == true || m_Items[index].mbIsPop == true)
            {
                m_Items.RemoveAt(index);
                if (m_Items.Count == 0)
                    return null;
                continue;
            }
            ++index;
        }

        int randomIndex = Random.Range(0, m_Items.Count);
        m_Items[randomIndex].mbIsPop = true;
        m_Items[randomIndex].SetItemActive(true);
        return m_Items[randomIndex];
    }

    public void PopChest(Vector2 _spawnPoint)
    {
        GameObject chest = Instantiate(m_ChestPrefab, this.transform);
        chest.transform.position = _spawnPoint;
    }
}
