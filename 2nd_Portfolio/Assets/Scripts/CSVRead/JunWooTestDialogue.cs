using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JunWooTestDialogue : MonoBehaviour
{
    [SerializeField] string[] csv_Filenames;
    [SerializeField] TextMeshProUGUI txt_Dialogue;
    [SerializeField] TextMeshProUGUI txt_Name;

    [SerializeField] Dialougue[] dialogues = null;
    DiaglogueParser Parser = null;
    int LineNumber=0;
    int dialogueNumber=0;
    private void Awake()
    {
        LineNumber = 0;//�� �ʱ�ȭ
        dialogueNumber = 0;
        Parser = GetComponent<DiaglogueParser>();
        dialogues = Parser.Parse(csv_Filenames[0]);//�� ó�� ���;��� ���(�Ƹ� Ʃ�丮��)
    }
    public void DialogueButtonClick()
    {
        if(LineNumber<dialogues.Length)
        {
            Debug.Log(dialogueNumber);
            Debug.Log(LineNumber);
            Debug.Log(dialogues[LineNumber].contexts.Length);
            if(dialogueNumber < dialogues[LineNumber].contexts.Length)
            {
                if (!dialogues[LineNumber].name.Equals(" "))
                {
                    txt_Name.text = dialogues[LineNumber].name;
                }
                txt_Dialogue.text = dialogues[LineNumber].contexts[dialogueNumber];
                dialogueNumber++;
            }
            else
            {
                LineNumber++;
                dialogueNumber = 0;
            }
        }
        else//��簡 ������ ���⼭ ��� â�� ���� ����� �ϴµ� �̰� ���߿�
        {
            txt_Dialogue.enabled = true;
            txt_Name.enabled = true;
        }
    }
    public void TestParseButton()
    {
        LineNumber = 0;
        dialogueNumber = 0;
    }
}
