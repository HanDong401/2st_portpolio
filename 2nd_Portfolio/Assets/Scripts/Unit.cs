using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [field : SerializeField] protected int m_CurrHp { get; set; }
    [field: SerializeField] protected int m_MaxHp { get; set; }
    [field: SerializeField] protected int m_Defense { get; set; }

    public int GetCurrHp()
    {
        return m_CurrHp;
    }

    public int GetMaxHp()
    {
        return m_MaxHp;
    }

    public void SetCurrHp(int _currHp)
    {
        this.m_CurrHp = _currHp;
    }

    public void SetMaxHp(int _maxHp)
    {
        this.m_MaxHp = _maxHp;
    }

}
