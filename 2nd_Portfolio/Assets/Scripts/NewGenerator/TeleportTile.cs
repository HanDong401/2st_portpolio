using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTile : MonoBehaviour
{
    [SerializeField] private RoomFirstDungeonGenerator RoomFirstDungeonGenerator;
    int dungeonLevel=0;
    private void Update()//��� �׽�Ʈ��
    {
        if(Input.GetKeyDown("j"))
        {
            DungeonLevelUp(dungeonLevel);
            DungeonClearAndGenerateNew();
            Debug.Log("�ߵ�");
        }
    }
    public void DungeonLevelGoZero(int _dungeonLevel)//���� ���۽� ���� ���� �ʱ�ȭ
    {
        _dungeonLevel = 0;
    }

    public void DungeonLevelUp(int _dungeonLevel)
    {
        _dungeonLevel++;
    }

    public void DungeonClearAndGenerateNew()
    {
        RoomFirstDungeonGenerator.GenerateDungeon();
    }
    //���⿡ �ݶ��̴� �����ϴ� �Լ� �����

}
