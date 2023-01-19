using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPNumText : MonoBehaviour
{
    Text m_HpNumText = null;

    public void HpNumTextAwake()
    {
        if (m_HpNumText == null)
            m_HpNumText = this.GetComponent<Text>();
    }

    public void SetHpText(int _currHp, int _maxHp)
    {
        m_HpNumText.text = $"{_currHp}/{_maxHp}";
    }
}
