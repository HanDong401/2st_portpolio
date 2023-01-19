using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOption : MonoBehaviour
{
    public delegate void MainMenuOptionEvent();
    private MainMenuOptionEvent m_BgmEvent = null;
    private MainMenuOptionEvent m_EffectEvent = null;
    [SerializeField] CheckPoint m_BgmButton, m_EffectButton = null;

    public void AddBgmEvent(MainMenuOptionEvent _callback)
    {
        m_BgmEvent = _callback;
    }

    public void AddEffectEvent(MainMenuOptionEvent _callback)
    {
        m_EffectEvent = _callback;
    }

    public void BgmOnOff()
    {

    }

    public void EffectOnOff()
    {

    }
}
