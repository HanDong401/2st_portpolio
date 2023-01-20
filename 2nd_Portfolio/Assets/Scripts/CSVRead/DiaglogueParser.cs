using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// �Ľ̵� �ؽ�Ʈ���� ��ġ�� �Լ�
    /// </summary>
    public Dialougue[] CombineParsingText(Dialougue[] _dialougues)
    {
        //���⼭ �ؾ��� ��
        //1.���� ���� �����̶� ���� �����̶� ���� ����� ���ϴ� ���� �ľ��ϰ�(" " üũ)
        //���� ����� ���ϴ� �Ŷ�� ���� �迭 ������ �־����
        //�ٸ� ����� ���ϴ� �Ÿ� �迭 �и�
        List<Dialougue> dialougueList = new List<Dialougue>(_dialougues);
        for(int i=0;  i< dialougueList.Count; i++)
        {
            //Debug.Log($"{i}��°" + _dialougues[i].name);
            //Debug.Log($"{i}��°" + _dialougues[i].contexts[0]);
            if (dialougueList[i].name == " ")
            {
                //���⼭ ��ġ��
                dialougueList[i - 1].contexts = dialougueList[i - 1].contexts.Concat(dialougueList[i].contexts).ToArray();
                //���ִ� ��
                dialougueList.RemoveAt(i);
                //Debug.Log($"i{i}��°"+dialougueList[i].name);
                //Debug.Log($"testcnt{testcnt}��°"+_dialougues[testcnt].name);
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