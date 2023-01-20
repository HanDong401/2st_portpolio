using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownToGoDungeon : MonoBehaviour, Interaction
{
    [SerializeField] private UIManager m_UIManager = null;
    [SerializeField] private Image[] NPCImage = null;
    private void OnEnable()
    {
        if (m_UIManager == null)
            m_UIManager = GameObject.FindObjectOfType<UIManager>();
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = false;
        }
    }
    public void InteractionExecute()
    {
        Debug.Log("이동성공");
        m_UIManager.OnMainStartEvent("DungeonScene");
        //SceneManager.LoadScene("");//이동할 씬 이름
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
}
