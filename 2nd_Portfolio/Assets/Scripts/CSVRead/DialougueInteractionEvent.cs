using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougueInteractionEvent : MonoBehaviour
{
    [SerializeField] private DialogueEvent dialogue;
    public Dialougue[] GetDialogue()
    {
        dialogue.dialougues = DataBaseManager.instance.GetDialougue((int)dialogue.line.x,(int)dialogue.line.y);
        Debug.Log("인터렉션이벤트 안 dialogue.dialougues.Length = " + dialogue.dialougues.Length);
        return dialogue.dialougues;
    }
}
