using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    HP m_HpUI = null;
    SP m_SpUI = null;

    private int m_currHp;
    private int m_maxHp;
    private int m_currSp;
    private int m_maxSp;

    private void Awake()
    {
        m_HpUI = GetComponentInChildren<HP>();
        m_SpUI = GetComponentInChildren<SP>();
    }

    public void InitUIManager(int _currHp = 100, int _maxHp = 100, int _currSp = 1, int _maxSp = 1)
    {
        this.m_currHp = _currHp;
        this.m_maxHp = _maxHp;
        this.m_currSp = _currSp;
        this.m_maxSp = _maxSp;
    }

    private void Update()
    {
        m_HpUI.UpdateHp(m_currHp, m_maxHp);
        m_SpUI.UpdateSp(m_currSp, m_maxSp);
    }
}
