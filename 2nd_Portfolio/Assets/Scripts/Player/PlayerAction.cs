using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public delegate void ActionAnimEvent();
    private ActionAnimEvent Action1Event = null;
    private ActionAnimEvent Action2Event = null;

    public void OnAction1(ActionCommand _action)
    {
        // ��� 1 �����Լ�
        if (_action != null)
            _action.ActionExecute();
        Debug.Log("PlayerAction1����!!");
    }

    public void OnAction2(ActionCommand _action)
    {
        // ��� 2 �����Լ�
        if (_action != null)
            _action.ActionExecute();
        Debug.Log("PlayerAction2����!!");
    }
}
