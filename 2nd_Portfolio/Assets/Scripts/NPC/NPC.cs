using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour, Interaction
{
    [Tooltip("0�� Ʃ�丮��, 1�� ���� NPC, 2���� NPC ����\n�ʼ��� ���� ��ȭ ��ũ��Ʈ�� �ʿ�� �ϹǷ�, ���� �ʰ����� �ʴ��� Ȯ���� ��!")]
    [SerializeField] private int IndexNum=0;//0 = Ʃ�丮��, 1= �׳� NPC
    [SerializeField] private JunWooTestDialogue JWDialogue=null;
    [SerializeField] private TextMeshProUGUI NPCText=null;
    bool flag_IsFirst = true;
    private void Start()
    {
        NPCText.enabled = false;
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
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //����ٰ� �ڽ��ݶ��̴� �ȿ� ������ �� Press 'E' to Interact ��� Ȱ��ȭ�ǰ� �������
        JWDialogue.SetNPCIndexNumber(IndexNum);
        JWDialogue.Parse();
        NPCText.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCText.enabled = false;
        JWDialogue.SetDialoguenumberZero();
        JWDialogue.TextGoDisable();
    }
}
