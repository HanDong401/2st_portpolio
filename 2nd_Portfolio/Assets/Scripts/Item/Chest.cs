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

    public void Awake()
    {
        this.m_Anim = GetComponent<Animator>();
        SetImage();
    }
    private void SetImage()
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
    
    public void SetPosition(Vector2 _pos)
    {
        this.transform.position = _pos;
    }

    public void AddChestEvent(ChestEvent _callback)
    {
        m_ChestEvent = _callback;
    }

    public void InteractionExecute()
    {
        if(mbIsOpen == false)
        {
            if (m_ChestEvent == null) return;
            mbIsOpen = true;
            m_Anim.SetBool("IsOpen", mbIsOpen);
            this.GetComponent<Collider2D>().enabled = false;
            Item popItem = m_ChestEvent();
            popItem.SetPosition(this.transform.position);

            Destroy(this.gameObject, 2f);
        }
    }
}
