using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerateManager : MonoBehaviour
{
    public delegate void MapGenerateEvent();
    private MapGenerateEvent m_MapGenerateEvent = null;
    private MapGenerateEvent m_MonsterSummonEvent = null;
    private MapGenerateEvent m_InitNodeEvent = null;
    private MapGenerateEvent m_RemoveAllMonsterEvent = null;
    public delegate void RoomPointEvent(Vector2Int[] _pos);
    private RoomPointEvent m_RoomPointEvent = null;
    public delegate void RoomManagerRandomMonsterEvent(Vector2 _pos);
    private RoomManagerRandomMonsterEvent m_RoomManageRandomMonsterEvent = null;
    public delegate Monster RoomManagerSelectMonsterEvent(string _kind, Vector2 _pos);
    private RoomManagerSelectMonsterEvent m_RoomManagerSelectMonsterEvent = null;
    public delegate void LoadSceneEvent(string _sceneName);
    private LoadSceneEvent m_LoadsceneEvent = null;
    [SerializeField] RoomFirstDungeonGenerator roomFirstDungeonGenerator = null;
    [SerializeField] MapTileVisualizer MapTileVisualizer= null;
    [SerializeField] Props props = null;
    [SerializeField] SpecialTileInstantiator specialTileInstantiator = null;
    [SerializeField] RoomManager m_RoomManager = null;

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
        if (m_RoomManager != null)
        {
            m_RoomManager.AddSummonRandomMonsterEvent(OnRoomManagerRandomMonster);
            m_RoomManager.AddSummonBossMonsterEvent(OnRoomManagerBossMonster);
            m_RoomManager.AddSummonSelectMonsterEvent(OnRoomManagerSelectMonster);
        }
    }
    public void DungeonGenerate()
    {
        m_RemoveAllMonsterEvent?.Invoke();
        MapTileVisualizer.Clear();
        //여기에 플레이어 위치 스타트 위치로 가는 함수 호출
        if(DungeonLevel < 3)
        {
            DungeonLevel++;
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(Random.Range(25, 40), Random.Range(10, 20));//최대 크기 제한하는 함수
            roomFirstDungeonGenerator.SetDungeonMinWidthHeight(5, 5);//방 최소크기 제한하는 함수
            //props.SetPropsCntFreely(15);
        }
        else if(DungeonLevel.Equals(3))
        {
            //보스룸 생성 1.
            DungeonLevel++;
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(20,15);
            roomFirstDungeonGenerator.SetDungeonWidthHeightBossRoom();
            //props.SetPropsCntFreely(0);
        }
        else if(DungeonLevel.Equals(4))
        {
            DungeonLevel = 0;

            //여기서 씬 전환 나와서 타운 맵으로 이동
            if (m_LoadsceneEvent != null)
                m_LoadsceneEvent("TownScene2");
            return;
        }
        roomFirstDungeonGenerator.GenerateDungeon();
        m_MapGenerateEvent?.Invoke();
        m_RoomManager.SetRooms(roomFirstDungeonGenerator.GetRoomCentersPos());
        //m_RoomPointEvent(null);
        //if (DungeonLevel < 3)
        //{
        //    m_RoomPointEvent(GetRoomCenterPos());
        //    m_MonsterSummonEvent?.Invoke();
        //}
        InitNode();
    }
    public Vector2 GetDoorPos()
    {
        return roomFirstDungeonGenerator.GetDoorPos();
    }
    public Vector2 GetStartPos()
    {
        return roomFirstDungeonGenerator.GetStartPos();
    }

    public Vector2[] GetRoomCenterPos()
    {
        Debug.Log("방위치 불러오기 실행");
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

    public void AddRemoveAllMonsterEvent(MapGenerateEvent _callback)
    {
        m_RemoveAllMonsterEvent = _callback;
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

    public void AddRoomManagerRandomMonsterEvent(RoomManagerRandomMonsterEvent _callback)
    {
        m_RoomManageRandomMonsterEvent = _callback;
    }

    public void AddRoomManagerSelectMonsterEvent(RoomManagerSelectMonsterEvent _callback)
    {
        m_RoomManagerSelectMonsterEvent = _callback;
    }

    public void OnRoomManagerRandomMonster(Vector2 _pos)
    {
        if (DungeonLevel < 4 && m_RoomManageRandomMonsterEvent != null)
            m_RoomManageRandomMonsterEvent(_pos);
    }

    public void OnRoomManagerBossMonster(Vector2 _pos)
    {
        if (DungeonLevel.Equals(4) && m_RoomManagerSelectMonsterEvent != null)
        {
            m_RoomManagerSelectMonsterEvent("Golem1", _pos);
        }
    }

    public void OnRoomManagerSelectMonster(string _name, Vector2 _pos)
    {
        m_RoomManagerSelectMonsterEvent(_name, _pos);
    }

    public void SetRoomManagerTargetPos(Vector2 _targetPos)
    {
        m_RoomManager.SetTargetPos(_targetPos);
    }


    public void InitNode()
    {
        m_InitNodeEvent?.Invoke();
    }

}
