using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestState : StateMachineBehaviour
{
    [SerializeField] TestAnim test = null;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        Debug.Log("OnStateMachineEnter");
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OnStateEnter");
        if (test == null)
            animator.gameObject.GetComponent<TestAnim>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (test != null)
        {
            if (Input.GetKey(KeyCode.D))
            {
                test.Test();
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OnStateEixt");
    }
}
