using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour, Interaction
{
    [Tooltip("0는 튜토리얼, 1은 마을 NPC, 2부터 NPC 자유\n필수로 엑셀 대화 스크립트를 필요로 하므로, 범위 초과하지 않는지 확인할 것!")]
    [SerializeField] private int IndexNum=0;//0 = 튜토리얼, 1= 그냥 NPC
    [SerializeField] private JunWooTestDialogue JWDialogue=null;
    [SerializeField] private Image[] NPCImage=null;
    [SerializeField] private GameObject ShopImage = null;

    bool flag_IsFirst = true; 
    private void Start()
    {
        for (int i = 0; i < NPCImage.Length; i++)
        {
            NPCImage[i].enabled = false;
        }
        if(ShopImage!=null)
        {
            ShopImage.SetActive(false);
        }
    }
    public void InteractionExecute()
    {
        //여기에다가 상호작용하면 대사창 출력되게 해야함
        if (flag_IsFirst == true)
        {
            JWDialogue.SetDialoguenumberZero();
            flag_IsFirst = false;
        }
        JWDialogue.DialogueButtonClick();
        //여기에서 불값 체크해서 대화가 끝났을 때 상점창이 켜지도록
        if (ShopImage != null&&JWDialogue.returnBoolIsDialogueEnd()==true)
        {
            ShopImage.SetActive(true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        JWDialogue.SetNPCIndexNumber(IndexNum);
        JWDialogue.Parse();
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
        JWDialogue.SetDialoguenumberZero();
        JWDialogue.TextGoDisable();
        if (ShopImage != null)
        {
            ShopImage.SetActive(false);
        }
    }
}
