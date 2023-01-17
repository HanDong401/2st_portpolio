using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaglogueParser : MonoBehaviour
{
    /// <summary>
    /// Parse(읽을 CSV파일 이름)
    /// </summary>
    public Dialougue[] Parse(string _CSVFileName)
    {
        List<Dialougue> dialogueList = new List<Dialougue>(); //대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] Data = csvData.text.Split(new char[] { '\n' });
        Debug.Log(Data.Length);
        for(int i = 1; i < Data.Length; i++)
        {
            string[] row = Data[i].Split(new char[] { ',' });

            Dialougue dialougue = new Dialougue();
            dialougue.name = row[1];

            List<string> contextList = new List<string>();
            do
            {
                contextList.Add(row[2]);
                if (i + 1 < Data.Length) ;
                else break;
            } while (row[0].ToString() == "");

            dialougue.contexts = contextList.ToArray();
            dialogueList.Add(dialougue);
        }
        return dialogueList.ToArray();
    }
    private void Start()
    {
        Parse("TestCSV1");
    }
}
