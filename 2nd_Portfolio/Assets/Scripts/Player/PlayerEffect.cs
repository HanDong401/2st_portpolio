using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] private PlayerDust m_Dust = null;

    public void InitDust(float _speed)
    {
        m_Dust.InitParticle(_speed);
    }

    public void SetDust(bool _isOnParticle)
    {
        m_Dust.SwitchParticle(_isOnParticle);
    }
}
