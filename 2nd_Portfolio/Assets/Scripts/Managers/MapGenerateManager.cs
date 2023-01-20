using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerateManager : MonoBehaviour
{
    [SerializeField] RoomFirstDungeonGenerator roomFirstDungeonGenerator = null;
    [SerializeField] MapTileVisualizer MapTileVisualizer= null;
    [SerializeField] Props props = null;
    [SerializeField] SpecialTileInstantiator specialTileInstantiator = null;

    [SerializeField] int DungeonLevel = 0;
    public void DungeonGenerate()
    {
        MapTileVisualizer.Clear();
        DungeonLevel = roomFirstDungeonGenerator.ReturnDungeonLevel();
        //여기에 플레이어 위치 스타트 위치로 가는 함수 호출
        if (DungeonLevel > 3)
        {
            //보스룸 생성 1.
            roomFirstDungeonGenerator.SetDungeonWidthHeightBossRoom();
        }
        roomFirstDungeonGenerator.GenerateDungeon();
        roomFirstDungeonGenerator.PlusDungeonLevel();
        
    }
    public Vector2 GetStartPos()
    {
        return roomFirstDungeonGenerator.GetStartPos();
    }
    
}
