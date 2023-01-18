using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaglogueParser : MonoBehaviour
{
    /// <summary>
    /// Parse(���� CSV���� �̸�)
    /// </summary>
    public Dialougue[] Parse(string _CSVFileName)
    {
        //Debug.Log(_CSVFileName.Length+"=parse ���ο��� ���� ������ ����");
        List<Dialougue> dialogueList = new List<Dialougue>(); //��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);
        string[] Data = csvData.text.Split(new char[] { '\n' });
        Debug.Log(csvData.text);
        for(int i = 1; i < Data.Length-1; i++)
        {
            string[] row = Data[i].Split(new char[] { ',' });
            Dialougue dialougue = new Dialougue();
            dialougue.name = row[1];

            List<string> contextList = new List<string>();
            do{
                contextList.Add(row[2]);
                if (i + 1 < Data.Length-1)
                {
                    ;
                }
                else break;
            } while (row[0].ToString() == "");

            dialougue.contexts = contextList.ToArray();
            dialogueList.Add(dialougue);
        }
        return dialogueList.ToArray();
    }
}
