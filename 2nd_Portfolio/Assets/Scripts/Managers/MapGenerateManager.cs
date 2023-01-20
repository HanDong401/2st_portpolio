using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerateManager : MonoBehaviour
{
    [SerializeField] RoomFirstDungeonGenerator roomFirstDungeonGenerator = null;
    [SerializeField] MapTileVisualizer MapTileVisualizer= null;
    [SerializeField] Props props = null;
    [SerializeField] SpecialTileInstantiator specialTileInstantiator = null;

    public void DungeonGenerate()
    {
        MapTileVisualizer.Clear();
        //���⿡ �÷��̾� ��ġ ��ŸƮ ��ġ�� ���� �Լ� ȣ��

        roomFirstDungeonGenerator.GenerateDungeon();
        
    }
    public Vector2 GetStartPos()
    {
        return roomFirstDungeonGenerator.GetStartPos();
    }
}
