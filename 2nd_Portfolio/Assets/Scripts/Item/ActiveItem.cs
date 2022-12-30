using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : Item, ActionCommand
{
    [SerializeField] protected Animator m_Anim = null;

    protected override void Interaction()
    {
        // �÷��̾ �̰��� ������ �����ؾ��� 
        // �����ؾ� �ϴ� ����
        // ActionCommandŸ�� Ŭ���� ��ü
        // �޾ƾ��ϴ� ����
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
