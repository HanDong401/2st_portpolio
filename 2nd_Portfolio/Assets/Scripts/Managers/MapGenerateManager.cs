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
        //���⿡ �÷��̾� ��ġ ��ŸƮ ��ġ�� ���� �Լ� ȣ��
        if (DungeonLevel >= 3 && DungeonLevel<4)
        {
            //������ ���� 1.
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(20,15);
            roomFirstDungeonGenerator.SetDungeonWidthHeightBossRoom();
            //props.SetPropsCntFreely(0);
        }
        else if(DungeonLevel < 3)
        {
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(Random.Range(25, 75), Random.Range(25, 50));//�ִ� ũ�� �����ϴ� �Լ�
            roomFirstDungeonGenerator.SetDungeonMinWidthHeight(Random.Range(4,8), Random.Range(4, 8));//�� �ּ�ũ�� �����ϴ� �Լ�
            //props.SetPropsCntFreely(15);
        }
        else if(DungeonLevel>=4)
        {
            roomFirstDungeonGenerator.SetDungeonLevelZero();
            
            //���⼭ �� ��ȯ ���ͼ� Ÿ�� ������ �̵�
            
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
