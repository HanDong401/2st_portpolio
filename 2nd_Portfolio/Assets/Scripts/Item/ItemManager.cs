using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using static Chest;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> m_Items;
    [SerializeField] private List<Item> m_ActiveItem;
    [SerializeField] private List<Item> m_PassiveItem;
    [SerializeField] Chest m_ChestPrefab = null;
    [SerializeField] private Inventory m_Inventory = null;
    public void ItemManagerAwake()
    {
        m_Items = GetComponentsInChildren<Item>().ToList();
        foreach(var item in m_Items)
        {
            item.SetPosition(this.transform.position);
            item.SetInventory(m_Inventory);
            //if (item is ActiveItem)
            //{
            //    Debug.Log("액티브아이템 넣기")
            //    m_ActiveItem.Add(item);
            //}
            //else if (item is PassiveItem)
            //{
            //    m_PassiveItem.Add(item);
            //}
            //item.SetItemActive(false);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // 아이템목록중 가져올수 있는 목록을 랜덤으로 뽑기
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
                //m_ActiveItem.Remove(m_Items[index]);
                //m_PassiveItem.Remove(m_Items[index]);
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

    public void SetInventory(Inventory _inven)
    {
        m_Inventory = _inven;
    }

    //public Item RandomPopActiveItem()
    //{
    //    if (m_ActiveItem.Count == 0)
    //        return null;

    //    int index = 0;

    //    while (index < m_ActiveItem.Count)
    //    {
    //        if (m_ActiveItem[index].mbIsPickUp == true || m_ActiveItem[index].mbIsPop == true)
    //        {
    //            m_Items.Remove(m_ActiveItem[index]);
    //            m_ActiveItem.Remove(m_ActiveItem[index]);
    //            if (m_ActiveItem.Count == 0)
    //                return null;
    //            continue;
    //        }
    //        ++index;
    //    }

    //    int randomIndex = Random.Range(0, m_ActiveItem.Count);
    //    m_ActiveItem[randomIndex].mbIsPop = true;
    //    m_ActiveItem[randomIndex].SetItemActive(true);
    //    return m_ActiveItem[randomIndex];
    //}

    //public Item RandomPopPassiveItem()
    //{
    //    if (m_PassiveItem.Count == 0)
    //        return null;

    //    int index = 0;

    //    while (index < m_PassiveItem.Count)
    //    {
    //        if (m_PassiveItem[index].mbIsPickUp == true || m_PassiveItem[index].mbIsPop == true)
    //        {
    //            m_Items.Remove(m_PassiveItem[index]);
    //            m_PassiveItem.Remove(m_PassiveItem[index]);
    //            if (m_PassiveItem.Count == 0)
    //                return null;
    //            continue;
    //        }
    //        ++index;
    //    }

    //    int randomIndex = Random.Range(0, m_PassiveItem.Count);
    //    m_PassiveItem[randomIndex].mbIsPop = true;
    //    m_PassiveItem[randomIndex].SetItemActive(true);
    //    return m_PassiveItem[randomIndex];

    //}

    //public Item SelectPopItem(string _item)
    //{
    //    switch(_item)
    //    {
    //        case "Item":
    //            return RandomPopItem();
    //        case "ActiveItem":
    //            return RandomPopActiveItem();
    //        case "PassiveItem":
    //            return RandomPopPassiveItem();
    //    }
    //    return null;
    //}    

    public void PopChest(Vector2 _spawnPoint)
    {
        Chest chest = Instantiate(m_ChestPrefab);
        chest.transform.position = _spawnPoint;
        chest.AddChestEvent(RandomPopItem);
    }
}
