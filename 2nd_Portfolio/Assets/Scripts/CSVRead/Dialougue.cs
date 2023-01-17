using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialougue
{
    [Tooltip("캐릭터")] public string name;
    [Tooltip("대사 내용")]public string[] contexts;
}
[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialougue[] dialougues;
}