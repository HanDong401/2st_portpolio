using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougueInteractionEvent : MonoBehaviour
{
    [SerializeField] private DialogueEvent dialogue;
    public Dialougue[] GetDialougue()
    {
        dialogue.dialougues = DataBaseManager.instance.GetDialougue((int)dialogue.line.x,(int)dialogue.line.y);
        return dialogue.dialougues;
    }
}
