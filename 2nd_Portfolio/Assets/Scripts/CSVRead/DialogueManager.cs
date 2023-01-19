using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] TextMeshProUGUI txt_Dialogue;
    [SerializeField] TextMeshProUGUI txt_Name;
    

    Dialougue[] dialogues;
    bool isDialogue = false;//��ȭ ���� ��� true
    bool isNext = false;//��ǲ �Է� ���

    int lineCnt = 0;//��ȭ ī��Ʈ
    int contextCnt = 0;//��� ī��Ʈ

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("��ư �Է� �׽�Ʈ");
        }
        if (isNext==true&&isDialogue==true&&Input.GetMouseButtonDown(0))
        {
            //Debug.Log("aaaaa");
            isNext = false;
            txt_Dialogue.text = "";
            if (++contextCnt < dialogues[lineCnt].contexts.Length)
            {
                StartCoroutine(TypeWriter());
            }
            else
            {
                contextCnt = 0;
                if(++lineCnt<dialogues.Length)
                {
                    StartCoroutine(TypeWriter());
                }
            }
        }
    }
    public void ShowDialogue(Dialougue[] _dialougues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        dialogues = _dialougues;

        StartCoroutine(TypeWriter());
    }
    IEnumerator TypeWriter()
    {
        SettingUI(true);
        string t_ReplaceText = dialogues[lineCnt].contexts[contextCnt];
        t_ReplaceText = t_ReplaceText.Replace("'",",");
        txt_Dialogue.text = t_ReplaceText;

        isNext = true;
        yield return null;
    }

    public void ButtonSettinUIGoTrue()
    {
        Debug.Log("��ư �Է� ����");
        SettingUI(true);
    }
    void SettingUI(bool _flag)
    {
        Debug.Log(_flag);
        go_DialogueBar.SetActive(_flag);
        go_DialogueNameBar.SetActive(_flag);
        txt_Dialogue.enabled = _flag;
        txt_Name.enabled = _flag;
    }
}
