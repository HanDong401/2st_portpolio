using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOption : Option
{
    private OptionEvent m_BgmEvent = null;
    private OptionEvent m_EffectEvent = null;
    [SerializeField] CheckPoint m_BgmButton, m_EffectButton = null;

    public void AddBgmEvent(OptionEvent _callback)
    {
        m_BgmEvent = _callback;
    }

    public void AddEffectEvent(OptionEvent _callback)
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
