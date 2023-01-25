using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    [SerializeField] private MonsterManager m_MonsterManager = null;
    [SerializeField] private ItemManager m_ItemManager = null;
    [SerializeField] private Door m_Door = null;

    private void OnEnable()
    {
        if(m_MonsterManager == null)
            m_MonsterManager = GameObject.FindObjectOfType<MonsterManager>();
        if(m_ItemManager == null)
            m_ItemManager = GameObject.FindObjectOfType<ItemManager>();
        if(m_Door == null)
            m_Door = GameObject.FindObjectOfType<Door>();
        StartCoroutine(CheckMonster());
    }

    IEnumerator CheckMonster()
    {
        while(true)
        {
            if (m_MonsterManager != null)
            {
                if (m_MonsterManager.GetMonsterListCount() <= 0)
                {
                    //m_ItemManager.PopChest(GameObject.FindGameObjectWithTag("Player").transform.position);
                    m_Door.DoorOpen();
                    yield break;
                }
            }
            yield return null;
        }
    }

    private void Update()
    {
    }
}
