using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, Interaction
{
    public delegate void DoorEvent(string _name);
    protected DoorEvent m_DoorEvent = null;
    protected Animator m_Anim = null;
    [SerializeField] protected Image[] NPCImage = null;
    [SerializeField] protected bool mbIsDoorOpen = false;

    public void DoorAwake()
    {
        m_Anim = this.GetComponent<Animator>();
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = false;
        }
        SubAwake();
    }

    protected virtual void SubAwake()
    {

    }
    public void InteractionExecute()
    {
        if (mbIsDoorOpen.Equals(true))
            SubExecute();
    }

    protected virtual void SubExecute()
    {

    }

    public void DoorOpen()
    {
        if (m_Anim == null) return;
        mbIsDoorOpen = true;
        m_Anim.SetBool("IsOpen", mbIsDoorOpen);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = false;
        }
    }

    public void AddDoorEvent(DoorEvent _callback)
    {
        m_DoorEvent = _callback;
    }
}
