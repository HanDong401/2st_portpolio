using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{
    private void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("TestCSV");//���� ���� ���� �̸�
        for(int i=0;i<data.Count;i++)
        {
            Debug.Log(data[i]["Index"].ToString());
            Debug.Log(data[i]["Item"].ToString());
            Debug.Log(data[i]["Content"].ToString());
        }
    }
}
