using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemUI : MonoBehaviour
{
    private GolemHP m_GolemHp = null;
    private int m_CurrHp = 0;
    private int m_MaxHp = 0;

    private void Awake()
    {
        m_GolemHp = GetComponentInChildren<GolemHP>();
    }

    private void Update()
    {
        m_GolemHp.SetHpGauge(m_CurrHp, m_MaxHp);
    }

    [Tooltip ("asdf")]
    public void InitGolemUI(int _currHp, int _maxHp)
    {
        m_CurrHp = _currHp;
        m_MaxHp = _maxHp;
    }
}
