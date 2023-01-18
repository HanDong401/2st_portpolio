using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    DialogueManager DialogueManager;
    DialougueInteractionEvent DialougueInteractionEvent;
    private void Start()
    {
        DialogueManager = GetComponent<DialogueManager>();
        DialougueInteractionEvent = GetComponent<DialougueInteractionEvent>();
    }

    public void Interact()
    {
        DialogueManager.ShowDialogue(DialougueInteractionEvent.GetDialogue());
    }
}
