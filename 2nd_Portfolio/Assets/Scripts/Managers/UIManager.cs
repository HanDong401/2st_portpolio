using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] MainUI m_Main = null;
    [SerializeField] Inventory m_Inventory = null;

    private int m_CurrHp;
    private int m_MaxHp;
    private int m_CurrSp;
    private int m_MaxSp;

    public void UIManagerAwake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (m_Main == null)
        {
            m_Main = this.GetComponentInChildren<MainUI>();
            if (m_Main != null)
            {
                m_Main.MainUIAwake();
                StartCoroutine(SetMainUI());
            }
        }
        //if (m_Inventory == null)
        //{
        //    m_Inventory = this.GetComponentInChildren<Inventory>();
        //    if (m_Inventory != null)
        //    {
        //        // 인벤토리 초기화
        //    }
        //}
    }

    public void ActiveMainUI(bool _bool)
    {
        m_Main.gameObject.SetActive(_bool);
    }

    public void ActiveInventory(bool _bool)
    {
        m_Inventory.gameObject.SetActive(_bool);
    }

    public void InitUIManager(int _currHp = 100, int _maxHp = 100, int _currSp = 1, int _maxSp = 1)
    {
        this.m_CurrHp = _currHp;
        this.m_MaxHp = _maxHp;
        this.m_CurrSp = _currSp;
        this.m_MaxSp = _maxSp;
    }

    IEnumerator SetMainUI()
    {
        while(true)
        {
            m_Main.SetHp(m_CurrHp, m_MaxHp);
            m_Main.SetSp(m_CurrSp, m_MaxSp);
            yield return null;
        }
    }
}
