using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestState : StateMachineBehaviour
{
    
    static int enterCount = 0;
    static int exitCount = 0;
    float time = 0f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OnStateEnter");
        animator.ResetTrigger("EnterState");
        enterCount++;
        time = 0f;
        Debug.Log(name);
        Debug.Log(GetInstanceID());
        Debug.Log(ToString());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time > 3f)
            animator.SetTrigger("ExitState");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OnStateEixt");
        exitCount++;
    }
}
