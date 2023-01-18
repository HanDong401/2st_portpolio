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
        LineNumber = 0;//값 초기화
        dialogueNumber = 0;
        Parser = GetComponent<DiaglogueParser>();
        dialogues = Parser.Parse(csv_Filenames[0]);//맨 처음 나와야할 대사(아마 튜토리얼)
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
        else//대사가 끝나면 여기서 대사 창을 종료 해줘야 하는데 이건 나중에
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
