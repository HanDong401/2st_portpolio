using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator m_Anim = null;

    public void SetAnim(Animator _anim)
    {
        m_Anim = _anim;
    }

    private void ResetAction()
    {
        m_Anim.ResetTrigger("IsOnSword");
        m_Anim.ResetTrigger("IsOnBow");
        m_Anim.ResetTrigger("IsOnMagic");
    }
}
