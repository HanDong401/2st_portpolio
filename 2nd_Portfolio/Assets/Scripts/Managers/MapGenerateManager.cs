using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerateManager : MonoBehaviour
{
    public delegate void MapGenerateEvent();
    private MapGenerateEvent mapGenerateEvent = null;
    [SerializeField] RoomFirstDungeonGenerator roomFirstDungeonGenerator = null;
    [SerializeField] MapTileVisualizer MapTileVisualizer= null;
    [SerializeField] Props props = null;
    [SerializeField] SpecialTileInstantiator specialTileInstantiator = null;

    [SerializeField] int DungeonLevel = 0;
    public void DungeonGenerate()
    {
        MapTileVisualizer.Clear();
        roomFirstDungeonGenerator.PlusDungeonLevel();
        DungeonLevel = roomFirstDungeonGenerator.ReturnDungeonLevel();
        //여기에 플레이어 위치 스타트 위치로 가는 함수 호출
        if (DungeonLevel >= 3 && DungeonLevel<4)
        {
            //보스룸 생성 1.
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(20,15);
            roomFirstDungeonGenerator.SetDungeonWidthHeightBossRoom();
            //props.SetPropsCntFreely(0);
        }
        else if(DungeonLevel < 3)
        {
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(Random.Range(25, 75), Random.Range(25, 50));//최대 크기 제한하는 함수
            roomFirstDungeonGenerator.SetDungeonMinWidthHeight(Random.Range(4,8), Random.Range(4, 8));//방 최소크기 제한하는 함수
            //props.SetPropsCntFreely(15);
        }
        else if(DungeonLevel>=4)
        {
            roomFirstDungeonGenerator.SetDungeonLevelZero();
            
            //여기서 씬 전환 나와서 타운 맵으로 이동
            
            return;
        }
        roomFirstDungeonGenerator.GenerateDungeon();
        mapGenerateEvent?.Invoke();
    }
    public Vector2 GetStartPos()
    {
        return roomFirstDungeonGenerator.GetStartPos();
    }
    
    public void AddMapGenerateEvent(MapGenerateEvent _callback)
    {
        mapGenerateEvent = _callback;
    }
}
