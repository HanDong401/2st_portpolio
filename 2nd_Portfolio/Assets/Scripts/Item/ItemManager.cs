using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using static Chest;
using MoreMountains.TopDownEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> m_Items;
    [SerializeField] private List<Item> m_ActiveItem;
    [SerializeField] private List<Item> m_PassiveItem;
    [SerializeField] private Chest m_NormalChestPrefab = null;
    [SerializeField] private Chest m_PassiveChestPrefab = null;
    [SerializeField] private Chest m_ActiveChestPrefab = null;
    [SerializeField] private DropItem m_CoinPrefab = null;
    [SerializeField] private DropItem m_HpUpPrefab = null;
    [SerializeField] private Inventory m_Inventory = null;
    private Coroutine m_ItemCoroutine = null;
    public void ItemManagerAwake()
    {
        m_Items = GetComponentsInChildren<Item>().ToList();
        foreach(var item in m_Items)
        {
            item.SetPosition(this.transform.position);
            item.SetInventory(m_Inventory);
        }
        if (m_ItemCoroutine == null)
            m_ItemCoroutine = StartCoroutine(SetItemList());
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator SetItemList()
    {
        int index = 0;
        while(true)
        {
            if (index >= m_Items.Count) yield break;

            if (m_Items[index] is ActiveItem)
                m_ActiveItem.Add(m_Items[index]);
            else if (m_Items[index] is PassiveItem)
                m_PassiveItem.Add(m_Items[index]);
            ++index;
            yield return null;
        }
    }

    IEnumerator DropCoin()
    {
        int dropCount = Random.Range(1, 10);

        for (int i = 0; i < dropCount; ++i)
        {
            DropItem go = Instantiate(m_CoinPrefab);

            go.transform.position = this.transform.position;
            go.DropObject();
            yield return new WaitForSeconds(0.1f);
            go.SetFollowing(true);
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

    public Item RandomPopActiveItem()
    {
        if (m_ActiveItem.Count == 0)
            return null;

        int index = 0;

        while (index < m_ActiveItem.Count)
        {
            if (m_ActiveItem[index].mbIsPickUp == true || m_ActiveItem[index].mbIsPop == true)
            {
                m_ActiveItem.RemoveAt(index);
                if (m_ActiveItem.Count == 0)
                    return null;
                continue;
            }
            ++index;
        }

        int randomIndex = Random.Range(0, m_ActiveItem.Count);
        m_ActiveItem[randomIndex].mbIsPop = true;
        m_ActiveItem[randomIndex].SetItemActive(true);
        return m_ActiveItem[randomIndex];
    }

    public Item RandomPopPassiveItem()
    {
        if (m_PassiveItem.Count == 0)
            return null;

        int index = 0;

        while (index < m_PassiveItem.Count)
        {
            if (m_PassiveItem[index].mbIsPickUp == true || m_PassiveItem[index].mbIsPop == true)
            {
                m_PassiveItem.RemoveAt(index);
                if (m_PassiveItem.Count == 0)
                    return null;
                continue;
            }
            ++index;
        }

        int randomIndex = Random.Range(0, m_PassiveItem.Count);
        m_PassiveItem[randomIndex].mbIsPop = true;
        m_PassiveItem[randomIndex].SetItemActive(true);
        return m_PassiveItem[randomIndex];
    }

    public void SetInventory(Inventory _inven)
    {
        m_Inventory = _inven;
    }

    public void PopChest(Vector2 _spawnPoint, string _kind = "All")
    {
        Chest chest = Instantiate(m_NormalChestPrefab);
        chest.transform.position = _spawnPoint;

        //switch (_kind)
        //{
        //    case "Active":
        //        chest.AddChestEvent(RandomPopActiveItem);
        //        break;
        //    case "Passive":
        //        chest.AddChestEvent(RandomPopPassiveItem);
        //        break;
        //    default:
        //        chest.AddChestEvent(RandomPopItem);
        //        break;
        //}
    }
}
