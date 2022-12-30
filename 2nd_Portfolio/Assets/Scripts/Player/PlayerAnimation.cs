using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    enum eAnimType
    {
        Sword,
        Bow,
        Magic
    }

    AnimatorController m_CurrAnim = null;
    [SerializeField] AnimatorController m_BaseAnim = null;
    [SerializeField] AnimatorController m_SwordAnim = null;
    [SerializeField] AnimatorController m_BowAnim = null;
    [SerializeField] AnimatorController m_MagicAnim = null;

    private void Awake()
    {
        m_CurrAnim = m_BaseAnim;
    }

    public void ResetAnim(Animator _anim)
    {
        _anim.ResetTrigger("IsOnSword");
        _anim.ResetTrigger("IsOnBow");
        _anim.ResetTrigger("IsOnMagic");
    }

    public AnimatorController GetAnim()
    {
        return m_CurrAnim;
    }
}
