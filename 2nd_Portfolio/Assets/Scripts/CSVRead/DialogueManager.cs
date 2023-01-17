using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] TextMeshPro txt_Dialogue;
    [SerializeField] TextMeshPro txt_Name;

    Dialougue[] dialogues;

    bool isDialogue = false;

    public void ShowDialogue(Dialougue[] _dialougues)
    {
        txt_Dialogue.text = "";
        txt_Name.text = "";
        dialogues = _dialougues;
    }
    public void Update()
    {
        ShowDialogue(DataBaseManager.instance.GetDialougue(0,10));
    }
}
