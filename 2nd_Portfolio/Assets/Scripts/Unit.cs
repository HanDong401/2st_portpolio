using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected int m_CurrHp;
    [Header ("스테이터스")]
    [Space (10f)]
    [Tooltip ("현재 유닛의 최대체력")]
    [SerializeField] protected int m_MaxHp;
    [Space (10f)]
    [Tooltip ("현재 유닛의 방어력")]
    [SerializeField] protected int m_Defense;

    public int GetCurrHp()
    {
        return m_CurrHp;
    }

    public int GetMaxHp()
    {
        return m_MaxHp;
    }

    public int GetDefense()
    {
        return m_Defense;
    }

    public void SetCurrHp(int _currHp)
    {
        this.m_CurrHp += _currHp;
        if (m_CurrHp > m_MaxHp)
            m_CurrHp = m_MaxHp;
    }

    public void SetMaxHp(int _maxHp)
    {
        this.m_MaxHp = _maxHp;
    }

    public void SetDefense(int _defense)
    {
        this.m_Defense = _defense;
    }

    public void OnDamage(int _damage)
    {
        int damage = _damage - m_Defense;
        if (damage < 1)
            damage = 1;
        SetCurrHp(-damage);
        Debug.Log("현재" + this.gameObject.name + "체력" + m_CurrHp);
    }


}
