using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTile : MonoBehaviour
{
    [SerializeField] private RoomFirstDungeonGenerator RoomFirstDungeonGenerator;
    int dungeonLevel=0;
    private void Update()//기능 테스트용
    {
        if(Input.GetKeyDown("j"))
        {
            DungeonLevelUp(dungeonLevel);
            DungeonClearAndGenerateNew();
            Debug.Log("잘됨");
        }
    }
    public void DungeonLevelGoZero(int _dungeonLevel)//게임 시작시 던전 레벨 초기화
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
    //여기에 콜라이더 검출하는 함수 만들기

}
