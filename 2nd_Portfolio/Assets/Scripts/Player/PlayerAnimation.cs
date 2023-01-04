using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator m_PlayerAnim = null;

    private void Awake()
    {
        m_PlayerAnim = this.GetComponent<Animator>();
    }

    public Animator GetAnim()
    {
        return m_PlayerAnim;
    }

    public void ResetAnim()
    {
        m_PlayerAnim.ResetTrigger("IsOnSword");
        m_PlayerAnim.ResetTrigger("IsOnBow");
        m_PlayerAnim.ResetTrigger("IsOnMagic");
    }
}
