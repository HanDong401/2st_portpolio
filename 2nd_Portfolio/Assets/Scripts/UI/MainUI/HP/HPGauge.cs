using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGauge : MonoBehaviour
{
    Image m_HpImage = null;

    private void Awake()
    {
        m_HpImage = this.GetComponent<Image>();
    }

    public void SetHp(int _currHp, int _maxHp)
    {
        if (_maxHp != 0)
        m_HpImage.fillAmount = (float)_currHp / _maxHp;
    }    
}