using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, Interaction
{
    [SerializeField] private Image[] NPCImage;
    public delegate Item ChestEvent();
    ChestEvent m_ChestEvent = null;
    Animator m_Anim = null;
    private bool mbIsOpen = false;

    private void Awake()
    {
        this.m_Anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = false;
        }
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
    public void SetChestEvent(ChestEvent _callback)
    {
        m_ChestEvent = _callback;
    }

    public void InteractionExecute()
    {
        if(mbIsOpen == false)
        {
            mbIsOpen = true;
            this.GetComponent<Collider2D>().enabled = false;
            if (m_ChestEvent == null) return;
            m_Anim.SetBool("IsOpen", mbIsOpen);
            Item popItem = m_ChestEvent();

            popItem.SetPosition(this.transform.position);
            Destroy(this.gameObject, 2f);
        }
    }
}
