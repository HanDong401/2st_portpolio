using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour, Interaction
{
    [Tooltip("0�� Ʃ�丮��, 1�� ���� NPC, 2���� NPC ����\n�ʼ��� ���� ��ȭ ��ũ��Ʈ�� �ʿ�� �ϹǷ�, ���� �ʰ����� �ʴ��� Ȯ���� ��!")]
    [SerializeField] private int IndexNum=0;//0 = Ʃ�丮��, 1= �׳� NPC
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
        //���⿡�ٰ� ��ȣ�ۿ��ϸ� ���â ��µǰ� �ؾ���
        if (flag_IsFirst == true)
        {
            JWDialogue.SetDialoguenumberZero();
            flag_IsFirst = false;
        }
        JWDialogue.DialogueButtonClick();
        //���⿡�� �Ұ� üũ�ؼ� ��ȭ�� ������ �� ����â�� ��������
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
