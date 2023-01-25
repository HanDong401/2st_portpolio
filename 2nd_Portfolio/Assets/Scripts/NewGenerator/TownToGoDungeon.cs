using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownToGoDungeon : MonoBehaviour, Interaction
{
    public delegate void DoorEvent(string _name);
    private DoorEvent m_DoorEvent = null;
    [SerializeField] private Image[] NPCImage = null;
    //[SerializeField] private MainSceneManager m_MainSceneManager = null;
    private void OnEnable()
    {
        //m_MainSceneManager = GameObject.FindObjectOfType<MainSceneManager>();
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = false;
        }
    }
    public void InteractionExecute()
    {
        Debug.Log("이동성공");
        //m_MainSceneManager.LoadScene("DungeonScene");
        if (m_DoorEvent != null)
            m_DoorEvent("DungeonScene");
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
