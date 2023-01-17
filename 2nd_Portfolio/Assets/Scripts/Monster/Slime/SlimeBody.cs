using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBody : MonoBehaviour
{
    public delegate void SlimeBodyEvent();
    private SlimeBodyEvent m_SlimeBodyEvent = null;

    public void AddSlimeBodyEvent(SlimeBodyEvent _callback)
    {
        m_SlimeBodyEvent = _callback;
    }

    private void SlimeAttack1()
    {
        m_SlimeBodyEvent?.Invoke();
    }
}
