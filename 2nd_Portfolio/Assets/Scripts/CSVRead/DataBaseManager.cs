using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;
    [SerializeField] string csv_Filename;
    public Dictionary<int, Dialougue> dialogueDictionary = new Dictionary<int, Dialougue>();

    public static bool isFinish = false;
    private void Awake()
    {
        //Debug.Log(csv_Filename);
        if (instance == null)
        {
            instance = this;
            DiaglogueParser theParser = GetComponent<DiaglogueParser>();
            Dialougue[] dialogues = theParser.Parse(csv_Filename);
            for(int i=0; i<dialogues.Length; i++)
            {
                dialogueDictionary.Add(i+1,dialogues[i]);
            }
            isFinish = true;
        }
    }
    /// <summary>
    /// ��縦 �޾ƿ��� �Լ�(���ۼ���, ������)  
    /// Dialougue �迭�� �޾ƿü� ������, ������-���� ���ڸ�ŭ ������
    /// </summary>
    public Dialougue[] GetDialougue(int _StartNum, int _EndNum)
    {
        List<Dialougue> dialougueList = new List<Dialougue>();
        for(int i=0; i<_EndNum-_StartNum;i++)
        {
            //���⿡ �ȵ��°� ������;;;
            dialougueList.Add(dialogueDictionary[i+_StartNum]);
            Debug.Log("DataBaseManager �� dialougueList.Count = "+dialougueList.Count+$" i = {i} ");
        }
        return dialougueList.ToArray();
    }
}
