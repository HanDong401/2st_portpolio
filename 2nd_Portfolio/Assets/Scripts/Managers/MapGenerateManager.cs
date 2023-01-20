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
        //���⿡ �÷��̾� ��ġ ��ŸƮ ��ġ�� ���� �Լ� ȣ��
        if (DungeonLevel > 3)
        {
            //������ ���� 1.
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
