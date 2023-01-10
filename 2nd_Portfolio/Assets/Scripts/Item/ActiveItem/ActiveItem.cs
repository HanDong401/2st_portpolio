using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : Item, ActionCommand
{
    [SerializeField] protected Animator m_Anim = null;

    protected void InitActiveItem()
    {
        m_Anim = m_Player.GetAnim();
    }
    protected override void Interaction()
    {
        InitActiveItem();
        InteractionItem();
    }

    public void SetPlayer(Player _player)
    {
        this.m_Player = _player;
        SetAnim();
    }

    private void SetAnim()
    {
        this.m_Anim = m_Player.GetAnim();
    }

    public void ActionExecute()
    {
        Action();
    }

    protected virtual void InteractionItem()
    {

    }

    protected virtual void Action()
    {

    }

}
