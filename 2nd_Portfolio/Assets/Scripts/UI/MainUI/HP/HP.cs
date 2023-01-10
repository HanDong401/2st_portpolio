using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    HPGauge m_HpGauge = null;
    HPNumText m_HpNum = null;

    private void Awake()
    {
        m_HpGauge = this.GetComponentInChildren<HPGauge>();
        m_HpNum = this.GetComponentInChildren<HPNumText>();
    }

    public void UpdateHp(int _currHp, int _maxHp)
    {
        m_HpGauge.SetHp(_currHp, _maxHp);
        m_HpNum.SetHpText(_currHp, _maxHp);
    }
}
