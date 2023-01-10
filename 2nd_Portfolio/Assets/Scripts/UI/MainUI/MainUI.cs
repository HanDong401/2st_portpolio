using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] HP m_HpUI = null;
    [SerializeField] SP m_SpUI = null;

    private void Awake()
    {
        m_HpUI = GetComponentInChildren<HP>();
        m_SpUI = GetComponentInChildren<SP>();
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
