using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBaseState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("ExitState");
    }

    public void Base()
    {
        Debug.Log("BaseState");
    }

}
