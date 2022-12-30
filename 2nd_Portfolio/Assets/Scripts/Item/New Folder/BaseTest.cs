using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTest : MonoBehaviour, CommandTest
{
    ActionTest Action = null;

    public BaseTest()
    {

    }

    public BaseTest(ActionTest _action)
    {
        Action = _action;
    }

    public void Execute()
    {
        Action?.Test1();
    }
}
