using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] HP m_HpUI = null;
    [SerializeField] SP m_SpUI = null;

    public void MainUIAwake()
    {
        if (m_HpUI == null)
        {
            m_HpUI = this.GetComponentInChildren<HP>();
            if (m_HpUI != null)
                m_HpUI.HPAwake();
        }
        if (m_SpUI == null)
        {
            m_SpUI = this.GetComponentInChildren<SP>();
            if (m_SpUI != null)
                m_SpUI.SPAwake();
        }
    }

    public void SetHp(int _currHp, int _maxHp)
    {
        m_HpUI.UpdateHp(_currHp, _maxHp);
    }

    public void SetSp(int _currSp, int _maxSp)
    {
        m_SpUI.UpdateSp(_currSp, _maxSp);
    }
}
