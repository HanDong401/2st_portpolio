using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, Interaction
{
    public delegate void DoorEvent(string _name);
    protected DoorEvent m_DoorEvent = null;
    public delegate int MonsterListEvent();
    protected MonsterListEvent m_MonsterListEvent = null;
    protected Animator m_Anim = null;
    protected Coroutine m_CheckDoorOpenCoroutine = null;
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
        if (m_CheckDoorOpenCoroutine != null)
            StopCoroutine(m_CheckDoorOpenCoroutine);
        m_CheckDoorOpenCoroutine = StartCoroutine(CheckDoorOpen());
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

    public void DoorClose()
    {
        if (m_Anim == null) return;
        mbIsDoorOpen = false;
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

    public void AddMonsterListEvent(MonsterListEvent _callback)
    {
        m_MonsterListEvent = _callback;
    }

    IEnumerator CheckDoorOpen()
    {
        while(true)
        {
            if (m_MonsterListEvent().Equals(0))
            {
                DoorOpen();
            }
            else
            {
                DoorClose();
            }
            yield return null;
        }
    }
}
