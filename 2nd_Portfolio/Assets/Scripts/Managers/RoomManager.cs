using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class RoomNode
{
    public Vector2 m_RoomCenterPos;

    public delegate void SummonChestEvent(Vector2 _pos);
    private SummonChestEvent m_RoomNodeSummonChestEvent = null;
    public List<Monster> m_MonsterList = new List<Monster>();
    public bool mbIsInRoom = false;
    public bool mbIsClear = false;
    public bool mbIsSummonMonster = false;
    public bool mbIsItStart = false;
    public bool mbIsItDoor = false;
    public bool mbIsBossRoom = false;
    public Coroutine m_CheckMonsterDeathCoroutine = null;

    public void AddRoomNodeSummonChestEvent(SummonChestEvent _callback)
    {
        Debug.Log("AddRoomNodeSummonChestEvent");
        m_RoomNodeSummonChestEvent = _callback;
    }

    public IEnumerator CheckMonsterDeath()
    {
        while(true)
        {
            if (m_MonsterList.Count.Equals(0) && mbIsClear.Equals(false))
            {
                Debug.Log("방 클리어 완료");
                m_RoomNodeSummonChestEvent?.Invoke(m_RoomCenterPos);
                mbIsClear = true;
                yield break;
            }

            for (int i = 0; i < m_MonsterList.Count; ++i)
            {
                if (m_MonsterList[i] == null)
                    m_MonsterList.RemoveAt(i);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}

[System.Serializable]
public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<RoomNode> m_Rooms = new List<RoomNode>();
    [SerializeField] private RoomNode m_CurrRoom;
    [SerializeField] private float AreaRange = 3f;
    [SerializeField] private bool mbIsRoomChange = false;
    private Coroutine m_CheckInRoomCoroutine = null;
    private Coroutine m_CheckCurrRoomCoroutine = null;
    private Vector2 m_TargetPos;
    public delegate Vector2 RoomManagerRetrunPosEvent();
    private RoomManagerRetrunPosEvent m_GetStartPosEvent = null;
    private RoomManagerRetrunPosEvent m_GetDoorPosEvent = null;
    public delegate void RoomManagerSummonChestEvent(Vector2 _pos);
    private RoomManagerSummonChestEvent m_RoomManagerSummonChestEvent = null;
    public delegate Monster SummonMonsterEvent(Vector2 _pos);
    private SummonMonsterEvent m_SummonRandomMonsterEvent = null;
    private SummonMonsterEvent m_SummonBossMonsterEvent = null;
    public delegate void SummonSelectMonsterEvent(string _name, Vector2 _pos);
    private SummonSelectMonsterEvent m_SummonSelectMonsterEvent = null;

    public void SetRooms(Vector2[] _rooms)
    {
        if (m_Rooms.Count > 0)
        {
            for (int i = 0; i < m_Rooms.Count; ++i)
            {
                if (m_Rooms[i].m_CheckMonsterDeathCoroutine != null)
                    StopCoroutine(m_Rooms[i].m_CheckMonsterDeathCoroutine);
            }
        }
        m_Rooms.Clear();
        if (_rooms == null)
        {
            Debug.Log("중앙 위치 Null");
            return;
        }
        Vector2 StartPos = m_GetStartPosEvent();
        Vector2 DoorPos = m_GetDoorPosEvent();
        float minStartDis = float.MaxValue;
        float curStartDis = 0f;
        float minDoorDis = float.MaxValue;
        float curDoorDis = 0f;
        int startRoomNum = 0;
        int DoorRoomNum = 0;
        for (int i = 0; i < _rooms.Length; ++i)
        {
            m_Rooms.Add(new RoomNode());
            m_Rooms[i].m_RoomCenterPos = (_rooms[i] - new Vector2(0.5f, 0.5f));
            m_Rooms[i].AddRoomNodeSummonChestEvent(RoomManagerPopChest);
            curStartDis = Vector2.Distance(m_Rooms[i].m_RoomCenterPos, StartPos);
            if (minStartDis > curStartDis)
            {
                minStartDis = curStartDis;
                startRoomNum = i;
            }

            curDoorDis = Vector2.Distance(m_Rooms[i].m_RoomCenterPos, DoorPos);
            if (minDoorDis > curDoorDis)
            {
                minDoorDis = curDoorDis;
                DoorRoomNum = i;
            }
        }
        if (m_Rooms.Count.Equals(1))
            m_Rooms[0].mbIsBossRoom = true;
        m_Rooms[startRoomNum].mbIsItStart = true;
        m_Rooms[DoorRoomNum].mbIsItDoor = true;
        StartCheckInRoom();
        StartCheckCurrRoom();
    }
    public void StartCheckInRoom()
    {
        if (m_CheckInRoomCoroutine != null)
            StopCoroutine(m_CheckInRoomCoroutine);

        m_CheckInRoomCoroutine = StartCoroutine(CheckInRoom());
    }

    IEnumerator CheckInRoom()
    {
        while (true)
        {
            float minDis = float.MaxValue;
            float curDis = 0f;
            int roomNum = 0;
            for (int i = 0; i < m_Rooms.Count; ++i)
            {
                curDis = Vector2.Distance(m_Rooms[i].m_RoomCenterPos, m_TargetPos);
                if (minDis > curDis)
                {
                    minDis = curDis;
                    roomNum = i;
                }
            }
            m_Rooms[roomNum].mbIsInRoom = true;
            m_CurrRoom = m_Rooms[roomNum];
            
            yield return null;
        }
    }

    public void StartCheckCurrRoom()
    {
        if (m_CheckCurrRoomCoroutine != null)
            StopCoroutine(m_CheckCurrRoomCoroutine);
        m_CheckCurrRoomCoroutine = StartCoroutine(CheckCurrRoom());
    }

    IEnumerator CheckCurrRoom()
    {
        while(true)
        {
            yield return null;
            if (m_CurrRoom == null) continue;

            if (m_CurrRoom.mbIsInRoom.Equals(true))
            {
                if (m_CurrRoom.mbIsBossRoom.Equals(true) && m_CurrRoom.mbIsSummonMonster.Equals(false))
                {
                    SummonBossMonster(m_CurrRoom.m_RoomCenterPos);
                    m_CurrRoom.mbIsSummonMonster = true;
                    continue;
                }

                if (m_CurrRoom.mbIsSummonMonster.Equals(true) || m_CurrRoom.mbIsItStart.Equals(true) || m_CurrRoom.mbIsItDoor.Equals(true) || m_CurrRoom.mbIsClear.Equals(true))
                    continue;

                // 방입장 이밴트 실행
                SummonRandomMonster();
                m_CurrRoom.mbIsSummonMonster = true;
                m_CurrRoom.m_CheckMonsterDeathCoroutine = StartCoroutine(m_CurrRoom.CheckMonsterDeath());
            }
        }
    }

    public void RoomManagerPopChest(Vector2 _pos)
    {
        Debug.Log("RoomManagerPopChest");
        m_RoomManagerSummonChestEvent?.Invoke(_pos);
    }

    public void AddGetStartPosEvent(RoomManagerRetrunPosEvent _callback)
    {
        m_GetStartPosEvent = _callback;
    }

    public void AddGetDoorPosEvent(RoomManagerRetrunPosEvent _callback)
    {
        m_GetDoorPosEvent = _callback;
    }

    public void AddSummonRandomMonsterEvent(SummonMonsterEvent _callback)
    {
        m_SummonRandomMonsterEvent = _callback;
    }

    public void AddSummonSelectMonsterEvent(SummonSelectMonsterEvent _callback)
    {
        m_SummonSelectMonsterEvent = _callback;
    }

    public void AddRoomManagerSummonChestEvent(RoomManagerSummonChestEvent _callback)
    {
        Debug.Log("AddRoomManagerSummonChestEvent");
        m_RoomManagerSummonChestEvent = _callback;
    }

    public void SummonRandomMonster()
    {
        int randomNum = Random.Range(1, 3);

        for (int i = 0; i < randomNum; ++i)
        {
            Vector2 randomPos = Random.insideUnitCircle * 1f + m_CurrRoom.m_RoomCenterPos;

            Monster monster = m_SummonRandomMonsterEvent?.Invoke(randomPos);
            monster.SetSummonRoom(m_CurrRoom);
            m_CurrRoom.m_MonsterList.Add(monster);
        }
    }

    public void AddSummonBossMonsterEvent(SummonMonsterEvent _callback)
    {
        m_SummonBossMonsterEvent = _callback;
    }

    public void SummonBossMonster(Vector2 _pos)
    {
        m_SummonBossMonsterEvent?.Invoke(_pos);
    }

    public void SetTargetPos(Vector2 _targetPos)
    {
        m_TargetPos = _targetPos;
    }

    public RoomNode GetCurrRoomNode()
    {
        return m_CurrRoom;
    }

    private void OnDrawGizmos()
    {
        if (m_Rooms != null && m_Rooms.Count > 0)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.5f);

            foreach (var room in m_Rooms)
            {
                Gizmos.DrawCube(new Vector3(room.m_RoomCenterPos.x, room.m_RoomCenterPos.y, 0f), new Vector2(AreaRange, AreaRange));
            }
        }
    }
}
