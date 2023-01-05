using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    private Animator m_Anim = null;

    private void Awake()
    {
        m_Anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Anim.SetTrigger("EnterState");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

        }
    }

    public void Test()
    {
        Debug.Log("TestAnim");
    }
}
