using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private HPGauge m_HpGauge = null;
    private HPNumText m_HpNum = null;

    public void HPAwake()
    {
        if (m_HpGauge == null)
        {
            m_HpGauge = this.GetComponentInChildren<HPGauge>();
            if (m_HpGauge != null)
                m_HpGauge.HpGaugeAwake();
        }
        if (m_HpNum == null)
        {
            m_HpNum = this.GetComponentInChildren<HPNumText>();
            if (m_HpNum != null)
                m_HpNum.HpNumTextAwake();
        }
    }

    public void UpdateHp(int _currHp, int _maxHp)
    {
        m_HpGauge.SetHp(_currHp, _maxHp);
        m_HpNum.SetHpText(_currHp, _maxHp);
    }
}
