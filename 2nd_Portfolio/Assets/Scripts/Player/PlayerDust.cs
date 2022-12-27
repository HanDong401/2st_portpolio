using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDust : MonoBehaviour
{
    private ParticleSystem m_Particle = null;
    private void Awake()
    {
        m_Particle = this.GetComponent<ParticleSystem>();
    }

    public void InitParticle(float _speed)
    {
        var main = m_Particle.main;
        var emission = m_Particle.emission;
        int value = (int)(_speed * 0.3f + 0.5f);
        main.maxParticles = value;
        emission.rateOverTime = value;
    }

    public void SwitchParticle(bool _isOnParticle)
    {
        if (_isOnParticle == true)
            m_Particle.Play();
        else
            m_Particle.Stop();
    }
}
