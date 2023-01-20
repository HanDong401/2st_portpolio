using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialougue
{
    [Tooltip("ĳ���� �̸�")] public string name;
    [Tooltip("��� ����")]public string[] contexts;
}
[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialougue[] dialougues;
}