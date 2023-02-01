using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0 : MonoBehaviour
{
    [SerializeField] private ItemManager m_ItemManager = null;
    [SerializeField] private Vector2 m_ChestPos;

    private void OnEnable()
    {
        if (m_ItemManager == null)
            m_ItemManager = GameObject.FindObjectOfType<ItemManager>();
        if (m_ItemManager != null)
            m_ItemManager.PopActiveChest(m_ChestPos);
    }
}
