using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemHP : MonoBehaviour
{
    private Image m_HpGauge = null;

    private void Awake()
    {
        m_HpGauge = GetComponent<Image>();
    }

    public void SetHpGauge(int _currHp, int _maxHp)
    {
        if (_maxHp != 0)
            m_HpGauge.fillAmount = (float)_currHp / _maxHp;
    }
}
