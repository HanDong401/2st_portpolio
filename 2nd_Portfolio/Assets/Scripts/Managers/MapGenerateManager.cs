using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerateManager : MonoBehaviour
{
    [SerializeField] RoomFirstDungeonGenerator roomFirstDungeonGenerator = null;
    [SerializeField] MapTileVisualizer MapTileVisualizer= null;
    

    public void DungeonGenerate()
    {
        MapTileVisualizer.Clear();
        //여기에 플레이어 위치 스타트 위치로 가는 함수 호출

        roomFirstDungeonGenerator.GenerateDungeon();
        
    }
}
