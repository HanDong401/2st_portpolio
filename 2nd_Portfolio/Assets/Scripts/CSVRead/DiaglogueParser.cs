using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiaglogueParser : MonoBehaviour
{
    /// <summary>
    /// Parse(읽을 CSV파일 이름)
    /// </summary>
    public Dialougue[] Parse(string _CSVFileName)
    {
        //Debug.Log(_CSVFileName.Length+"=parse 내부에서 받은 파일의 길이");
        List<Dialougue> dialogueList = new List<Dialougue>(); //대사 리스트 생성
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

    /// <summary>
    /// 파싱된 텍스트들을 합치는 함수
    /// </summary>
    public Dialougue[] CombineParsingText(Dialougue[] _dialougues)
    {
        //여기서 해야할 것
        //1.먼저 이전 문장이랑 지금 문장이랑 같은 사람이 말하는 건지 파악하고(" " 체크)
        //같은 사람이 말하는 거라면 같은 배열 안으로 넣어야함
        //다른 사람이 말하는 거면 배열 분리
        List<Dialougue> dialougueList = new List<Dialougue>(_dialougues);
        for(int i=0;  i< dialougueList.Count; i++)
        {
            //Debug.Log($"{i}번째" + _dialougues[i].name);
            //Debug.Log($"{i}번째" + _dialougues[i].contexts[0]);
            if (dialougueList[i].name == " ")
            {
                //여기서 합치고
                dialougueList[i - 1].contexts = dialougueList[i - 1].contexts.Concat(dialougueList[i].contexts).ToArray();
                //없애는 거
                dialougueList.RemoveAt(i);
                //Debug.Log($"i{i}번째"+dialougueList[i].name);
                //Debug.Log($"testcnt{testcnt}번째"+_dialougues[testcnt].name);
                i--;
            }
            //else
            //{
            //    dialougueList.RemoveAt(i);
            //    i--;
            //}
        }
        return dialougueList.ToArray();
    }
}
