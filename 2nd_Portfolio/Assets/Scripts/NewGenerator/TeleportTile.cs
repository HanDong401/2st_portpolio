using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTile : MonoBehaviour
{
    [SerializeField] private RoomFirstDungeonGenerator RoomFirstDungeonGenerator;
    int dungeonLevel=0;
    public void TeleAwake()//��� �׽�Ʈ��
    {
        DontDestroyOnLoad(this);
        DungeonLevelUp();
        if (RoomFirstDungeonGenerator == null)
            RoomFirstDungeonGenerator = GameObject.FindObjectOfType<RoomFirstDungeonGenerator>();
        if (RoomFirstDungeonGenerator != null)
            DungeonClearAndGenerateNew();
        Debug.Log("�ߵ�");
    }
    public void TeleStart()
    {
        DungeonLevelUp();
    }
    public void DungeonLevelGoZero(int _dungeonLevel)//���� ���۽� ���� ���� �ʱ�ȭ
    {
        _dungeonLevel = 0;
    }

    public void DungeonLevelUp()
    {
        dungeonLevel++;
    }

    public void DungeonClearAndGenerateNew()
    {
        if (RoomFirstDungeonGenerator != null)
            RoomFirstDungeonGenerator.GenerateDungeon();
    }
    //���⿡ �ݶ��̴� �����ϴ� �Լ� �����

}
