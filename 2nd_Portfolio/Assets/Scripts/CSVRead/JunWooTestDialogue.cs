using System;
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
    int NPCIndexNum = 0;
    int LineNumber=0;
    int dialogueNumber=0;
    private void Awake()
    {
        LineNumber = 0;//값 초기화
        dialogueNumber = 0;
        Parser = GetComponent<DiaglogueParser>();
    }
    public void Parse()
    {
        ParseCSVFile(ref dialogues, NPCIndexNum);//맨 처음 나와야할 대사(아마 튜토리얼)
        TextGoDisable();
    }
    public void SetNPCIndexNumber(int _indexNum)
    {
        NPCIndexNum = _indexNum;
    }
    public void ParseCSVFile(ref Dialougue[] _dialougues,int _ParserCnt)
    {
        _dialougues = Parser.Parse(csv_Filenames[_ParserCnt]);
        TestParseButton(ref _dialougues);
    }
    public void DialogueButtonClick()
    {
        if (LineNumber<dialogues.Length)
        {
            Debug.Log(dialogues.Length); //3
            //여기서 사람 이름, 텍스트 켜기(이거 상호작용 키 누르면 들어가야됨 일단 지금은 테스트)
            TextGoEnable();
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
                    TextGoDisable();
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
            TextGoDisable();
        }
    }

    public void TextGoDisable()
    {
        txt_Dialogue.enabled = false;
        txt_Name.enabled = false;
    }

    public void TextGoEnable()
    {
        txt_Dialogue.enabled = true;
        txt_Name.enabled = true;
    }
    public void TestParseButton(ref Dialougue[] _dialougues)
    {
        Dialougue[] test = null;
        test = Parser.CombineParsingText(_dialougues);
        _dialougues = null;
        _dialougues = test;
    }
    public void SetDialoguenumberZero()
    {
        LineNumber = 0;
        dialogueNumber = 0;
    }
}
