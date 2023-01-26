using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerateManager : MonoBehaviour
{
    public delegate void MapGenerateEvent();
    private MapGenerateEvent m_MapGenerateEvent = null;
    private MapGenerateEvent m_MonsterSummonEvent = null;
    private MapGenerateEvent m_InitNodeEvent = null;
    public delegate void RoomPointEvent(Vector2Int[] _pos);
    private RoomPointEvent m_RoomPointEvent = null;
    public delegate void LoadSceneEvent(string _sceneName);
    private LoadSceneEvent m_LoadsceneEvent = null;
    [SerializeField] RoomFirstDungeonGenerator roomFirstDungeonGenerator = null;
    [SerializeField] MapTileVisualizer MapTileVisualizer= null;
    [SerializeField] Props props = null;
    [SerializeField] SpecialTileInstantiator specialTileInstantiator = null;

    [SerializeField] int DungeonLevel = 0;

    public void Start()
    {
        DungeonGenerate();
    }

    public void MapGenerateManagerAwake()
    {
        if (roomFirstDungeonGenerator == null)
            roomFirstDungeonGenerator = GameObject.FindObjectOfType<RoomFirstDungeonGenerator>();
        if (roomFirstDungeonGenerator != null)
            roomFirstDungeonGenerator.AddRoomEvent(InitNode);
    }
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
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(Random.Range(25, 40), Random.Range(10, 20));//�ִ� ũ�� �����ϴ� �Լ�
            roomFirstDungeonGenerator.SetDungeonMinWidthHeight(5, 5);//�� �ּ�ũ�� �����ϴ� �Լ�
            //props.SetPropsCntFreely(15);
        }
        else if(DungeonLevel>=4)
        {
            roomFirstDungeonGenerator.SetDungeonLevelZero();

            //���⼭ �� ��ȯ ���ͼ� Ÿ�� ������ �̵�
            if (m_LoadsceneEvent != null)
                m_LoadsceneEvent("TownScene2");
            return;
        }
        roomFirstDungeonGenerator.GenerateDungeon();
        m_RoomPointEvent(null);
        if (DungeonLevel < 3)
        {
            m_RoomPointEvent(GetRoomCenterPos());
            m_MonsterSummonEvent?.Invoke();
        }
        m_MapGenerateEvent?.Invoke();
    }
    public Vector2 GetStartPos()
    {
        return roomFirstDungeonGenerator.GetStartPos();
    }
    public Vector2Int[] GetRoomCentersPos()
    {
        return roomFirstDungeonGenerator.GetRoomCentersPos();
    }
    
    public void AddMapGenerateEvent(MapGenerateEvent _callback)
    {
        m_MapGenerateEvent = _callback;
    }

    public void AddMonsterSummonEvent(MapGenerateEvent _callback)
    {
        m_MonsterSummonEvent = _callback;
    }

    public void AddInitNodeEvent(MapGenerateEvent _callback)
    {
        m_InitNodeEvent = _callback;
    }

    public void AddLoadSceneEvent(LoadSceneEvent _callback)
    {
        m_LoadsceneEvent = _callback;
    }

    public void AddRoomPointEvent(RoomPointEvent _callback)
    {
        m_RoomPointEvent = _callback;
    }


    public void InitNode()
    {
        m_InitNodeEvent?.Invoke();
    }

    public Vector2Int[] GetRoomCenterPos()
    {
        Debug.Log("����ġ �ҷ����� ����");
        return roomFirstDungeonGenerator.GetRoomCentersPos();
    }
}
