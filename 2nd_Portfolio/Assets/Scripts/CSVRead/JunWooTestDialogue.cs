using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JunWooTestDialogue : MonoBehaviour
{
    [SerializeField] string[] csv_Filenames;
    [SerializeField] TextMeshProUGUI txt_Dialogue;
    [SerializeField] TextMeshProUGUI txt_Name;
    [SerializeField] private Image[] TextImage;

    [SerializeField] Dialougue[] dialogues = null;
    DiaglogueParser Parser = null;
    int NPCIndexNum = 0;
    int LineNumber = 0;
    int dialogueNumber = 0;
    bool flagDialogueEnd = false;
    private void Awake()
    {
        LineNumber = 0;//�� �ʱ�ȭ
        dialogueNumber = 0;
        Parser = GetComponent<DiaglogueParser>();
    }
    public void Parse()
    {
        ParseCSVFile(ref dialogues, NPCIndexNum);//�� ó�� ���;��� ���(�Ƹ� Ʃ�丮��)
        TextGoDisable();
    }
    public void SetNPCIndexNumber(int _indexNum)
    {
        NPCIndexNum = _indexNum;
    }
    public void ParseCSVFile(ref Dialougue[] _dialougues, int _ParserCnt)
    {
        _dialougues = Parser.Parse(csv_Filenames[_ParserCnt]);
        TestParseButton(ref _dialougues);
    }
    public void DialogueButtonClick()
    {
        if (LineNumber < dialogues.Length)
        {
            //Debug.Log(dialogues.Length);
            //���⼭ ��� �̸�, �ؽ�Ʈ �ѱ�(�̰� ��ȣ�ۿ� Ű ������ ���ߵ� �ϴ� ������ �׽�Ʈ)
            ChageBoolIsDialogueEnd(false);
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
                    //��ȭ ���� ��
                    ChageBoolIsDialogueEnd(true);
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
            //Debug.Log("LineNumber"+LineNumber);
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
        for (int i = 0; i < TextImage.Length; i++)
        {
            TextImage[i].enabled = false;
        }
    }
    /// <summary>
    /// true=��ȭ�� ����, false = ��ȭ�� �� ����
    /// </summary>
    public bool returnBoolIsDialogueEnd()
    {
        return flagDialogueEnd;
    }
    public void ChageBoolIsDialogueEnd(bool _bool)
    {
        flagDialogueEnd = _bool;
    }
    public void TextGoEnable()
    {
        txt_Dialogue.enabled = true;
        txt_Name.enabled = true;
        for(int i=0;i<TextImage.Length;i++)
        {
            TextImage[i].enabled = true;
        }
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
