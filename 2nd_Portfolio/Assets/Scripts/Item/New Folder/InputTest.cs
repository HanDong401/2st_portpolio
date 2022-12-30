using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    CommandTest Command = null;

    public void SetComaand(CommandTest _command)
    {
        Command = _command;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            Command?.Execute();
        }
    }
}
