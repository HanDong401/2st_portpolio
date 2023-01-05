using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory m_inventory;
    public static Inventory Instance
    {
        get
        {
            if (m_inventory == null)
                return null;
            return m_inventory;
        }
    }

    public List<PassiveItem> m_PassiveItems = new List<PassiveItem>();
    public ActionCommand m_Action1 = null;
    public ActionCommand m_Action2 = null;

    private void Awake()
    {
        if (m_inventory == null)
        {
            m_inventory = this;
        }
        else if (m_inventory != null)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void GetItem(Item _item)
    {
        if (_item is PassiveItem)
        {
            m_PassiveItems.Add((PassiveItem)_item);
        }
        else if (_item is ActiveItem)
        {
            SetActionItem(_item as ActionCommand);
        }
    }

    public void SetActionItem(ActionCommand _action)
    {
        if (m_Action1 == null)
            m_Action1 = _action;
        else if (m_Action2 == null)
            m_Action2 = _action;
        else if (m_Action1 != null)
            m_Action1 = _action;
        else if (m_Action2 != null)
            m_Action2 = _action;
    }

    public ActionCommand GetAction(int _index)
    {
        switch (_index)
        {
            case 1:
                return m_Action1;
            case 2:
                return m_Action2;
        }
        return null;
    }
}
