using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        ParseCSVFile(ref dialogues, 0);//맨 처음 나와야할 대사(아마 튜토리얼)
    }
    public void ParseCSVFile(ref Dialougue[] _dialougues,int _ParserCnt)
    {
        _dialougues = Parser.Parse(csv_Filenames[_ParserCnt]);
    }
    public void DialogueButtonClick()
    {
        if (LineNumber<dialogues.Length)
        {
            Debug.Log(dialogues.Length); //3
            //여기서 사람 이름, 텍스트 켜기(이거 상호작용 키 누르면 들어가야됨 일단 지금은 테스트)
            txt_Dialogue.enabled = true;
            txt_Name.enabled = true;

            //Debug.Log(dialogueNumber);
            //Debug.Log(LineNumber);
            //Debug.Log(dialogues[LineNumber].contexts.Length);
            if (dialogueNumber < dialogues[LineNumber].contexts.Length)
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
                if (LineNumber >= dialogues.Length)
                {
                    //대화 종료 시
                    txt_Dialogue.enabled = enabled;
                    txt_Name.enabled = enabled;
                    return;
                }
                dialogueNumber = 0;
                //test
                txt_Name.text = dialogues[LineNumber].name;
                txt_Dialogue.text = dialogues[LineNumber].contexts[dialogueNumber];
                dialogueNumber++;
            }
            //Debug.Log("dialogueNumber"+dialogueNumber);
            Debug.Log("LineNumber"+LineNumber);
        }
        else
        {
            txt_Dialogue.enabled = enabled;
            txt_Name.enabled = enabled;
        }
    }
    public void TestParseButton()
    {
        Dialougue[] test = null;
        test = Parser.CombineParsingText(dialogues);
        dialogues = null;
        dialogues = test;
    }
    public void SetDialoguenumberZero()
    {
        LineNumber = 0;
        dialogueNumber = 0;
    }
}
