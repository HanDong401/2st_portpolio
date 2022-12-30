using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : Item, ActionCommand
{
    [SerializeField] protected Animator m_Anim = null;

    protected override void Interaction()
    {
        // 플레이어에 이것의 정보를 전달해야함 
        // 전달해야 하는 정보
        // ActionCommand타입 클래스 전체
        // 받아야하는 정보
        // PlayerAnimator
        Debug.Log("ActiveItemInteraction!!");
    }

    public void SetAnim(Animator _anim)
    {
        m_Anim = _anim;
    }

    public void ActionExecute()
    {
        Action();
    }

    protected virtual void Action()
    {
        Debug.Log("BaseActionItem Action");
    }
}
