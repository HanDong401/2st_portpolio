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
    [SerializeField] private DropItem m_HeartPrefab = null;
    [SerializeField] private Inventory m_Inventory = null;
    [SerializeField] private List<DropItem> m_DropItems = new List<DropItem>();
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

    IEnumerator DropCoin(Vector2 _pos)
    {
        int dropCount = Random.Range(1, 10);

        for (int i = 0; i < dropCount; ++i)
        {
            DropItem item = Instantiate(m_CoinPrefab);
            m_DropItems.Add(item);
            item.SetPosition(_pos);
            item.DropObject();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DropHeart(Vector2 _pos)
    {
        DropItem item = Instantiate(m_HeartPrefab);
        m_DropItems.Add(item);
        item.SetPosition(_pos);
        item.DropObject();
    }


    public void RandomPopChest(Vector2 _pos)
    {
        int randomIndex = Random.Range(1, 101);

        if (randomIndex < 60)
        {
            // 일반상자 소환
            Chest chest = Instantiate(m_NormalChestPrefab);
            chest.AddChestEvent(RandomPopDropItem);
            chest.transform.position = _pos;
            Debug.Log("일반상자");
        }
        else if (randomIndex < 90)
        {
            // 패시브상자 소환
            Chest chest = Instantiate(m_PassiveChestPrefab);
            if (m_PassiveItem.Count.Equals(0))
                chest.AddChestEvent(RandomPopDropItem);
            else
                chest.AddChestEvent(RandomPopPassiveItem);
            chest.transform.position = _pos;
            Debug.Log("패시브상자");
        }
        else
        {
            //액티브상자 소환
            Chest chest = Instantiate(m_ActiveChestPrefab);
            if (m_ActiveItem.Count.Equals(0))
                chest.AddChestEvent(RandomPopDropItem);
            else
                chest.AddChestEvent(RandomPopActiveItem);
            chest.transform.position = _pos;
            Debug.Log("액티브상자");
        }
    }    

    // 아이템목록중 가져올수 있는 목록을 랜덤으로 뽑기
    public void RandomPopDropItem(Vector2 _pos)
    {
        int randomCoin = Random.Range(1, 3);
        for (int i = 0; i < randomCoin; ++i)
        {
            StartCoroutine(DropCoin(_pos));
        }

        int randomHeart = Random.Range(1, 101);

        if (randomHeart > 60)
        {
            DropHeart(_pos);
        }
    }

    public void RandomPopActiveItem(Vector2 _pos)
    {
        if (m_ActiveItem.Count == 0)
            return;

        int index = 0;

        while (index < m_ActiveItem.Count)
        {
            if (m_ActiveItem[index].mbIsPickUp == true || m_ActiveItem[index].mbIsPop == true)
            {
                m_ActiveItem.RemoveAt(index);
                if (m_ActiveItem.Count == 0)
                    return;
                continue;
            }
            ++index;
        }

        int randomIndex = Random.Range(0, m_ActiveItem.Count);
        m_ActiveItem[randomIndex].mbIsPop = true;
        m_ActiveItem[randomIndex].SetItemActive(true);
        m_ActiveItem[randomIndex].SetPosition(_pos);
        m_ActiveItem.RemoveAt(randomIndex);
    }

    public void RandomPopPassiveItem(Vector2 _pos)
    {
        if (m_PassiveItem.Count == 0)
            return;

        int index = 0;

        while (index < m_PassiveItem.Count)
        {
            if (m_PassiveItem[index].mbIsPickUp == true || m_PassiveItem[index].mbIsPop == true)
            {
                m_PassiveItem.RemoveAt(index);
                if (m_PassiveItem.Count == 0)
                    return;
                continue;
            }
            ++index;
        }

        int randomIndex = Random.Range(0, m_PassiveItem.Count);
        m_PassiveItem[randomIndex].mbIsPop = true;
        m_PassiveItem[randomIndex].SetItemActive(true);
        m_PassiveItem[randomIndex].SetPosition(_pos);
        m_PassiveItem.RemoveAt(randomIndex);
    }

    public void PopActiveChest(Vector2 _pos)
    {
        Chest chest = Instantiate(m_ActiveChestPrefab);
        chest.AddChestEvent(RandomPopActiveItem);
        chest.transform.position = _pos;
    }

    public void RemoveAllDropItem()
    {
        if (m_DropItems.Count.Equals(0)) return;
        foreach(var item in m_DropItems)
        {
            if (item != null)
                Destroy(item.gameObject);
        }
        m_DropItems.Clear();
    }

    public void SetInventory(Inventory _inven)
    {
        m_Inventory = _inven;
    }
}
