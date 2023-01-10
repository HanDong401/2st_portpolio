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


    private void Awake()
    {
        m_Main = GetComponentInChildren<MainUI>();
        m_Inventory = GetComponentInChildren<Inventory>();
    }

    private void Start()
    {
        if (m_Main != null)
            StartCoroutine(SetMainUI());
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
