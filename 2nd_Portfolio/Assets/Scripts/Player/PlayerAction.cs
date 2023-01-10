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
        // 기능 1 실행함수
        if (_action != null)
            _action.ActionExecute();
        Debug.Log("PlayerAction1실행!!");
    }

    public void OnAction2(ActionCommand _action)
    {
        // 기능 2 실행함수
        if (_action != null)
            _action.ActionExecute();
        Debug.Log("PlayerAction2실행!!");
    }
}
