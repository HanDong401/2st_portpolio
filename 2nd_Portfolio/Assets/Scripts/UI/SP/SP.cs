using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SP : MonoBehaviour
{
    Image[] m_SpGauge = null;

    private void Awake()
    {
        m_SpGauge = this.GetComponentsInChildren<Image>();
    }

    public void UpdateSp(int _currSp, int _maxSp)
    {
        if (_currSp > _maxSp)
            _currSp = _maxSp;

        for (int i = 0; i < _currSp; ++i)
        {
            m_SpGauge[i].enabled = true;
        }

        for (int i = _currSp; i < m_SpGauge.Length; ++i)
        {
            m_SpGauge[i].enabled = false;
        }
    }
}
