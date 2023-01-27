using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class RoomNode
{
    public Vector2 m_RoomCenterPos;

    public bool mbIsInRoom = false;
    public bool mbIsClear = false;
    public bool mbIsActiveEvent = false;
    public bool mbIsItStart = false;
    public bool mbIsItDoor = false;
}

[System.Serializable]
public class RoomManager : MonoBehaviour
{
    //[SerializeField] private RoomNode[] m_Rooms;
    [SerializeField] private List<RoomNode> m_Rooms;
    [SerializeField] private RoomNode m_CurrRoom;
    [SerializeField] private float AreaRange = 3f;
    private Coroutine m_Coroutine;
    private Vector2 m_TargetPos;
    private bool mbIsRoomChange = false;
    public delegate void SummonMonsterEvent(Vector2[] _pos);
    private SummonMonsterEvent m_MonsterEvent = null;

    public void SetRooms(Vector2[] _rooms)
    {
        m_Rooms.Clear();
        if (_rooms == null)
        {
            Debug.Log("Áß¾Ó À§Ä¡ Null");
            return;
        }
        m_Rooms = new List<RoomNode>();
        Vector2 StartPos = GameObject.FindGameObjectWithTag("START").transform.position;
        Vector2 DoorPos = GameObject.FindGameObjectWithTag("DOOR").transform.position;
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
            Debug.Log("·ë ¸Å´ÏÀú ¹Ýº¹¹® ¾È¿¡ µé¾î¿È");
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
        m_Rooms[startRoomNum].mbIsItStart = true;
        m_Rooms[DoorRoomNum].mbIsItDoor = true;
        StartCheckInRoom();
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
                //Debug.Log(m_TargetPos);
                //RaycastHit2D hit = Physics2D.Raycast(m_TargetPos, m_Rooms[i].m_RoomCenterPos - m_TargetPos);
                //if (hit.collider != null)
                //{
                //    m_Rooms[i].mbIsInRoom = false;
                //    continue;
                //}

                curDis = Vector2.Distance(m_Rooms[i].m_RoomCenterPos, m_TargetPos);
                if (minDis > curDis)
                {
                    minDis = curDis;
                    if (roomNum != i)
                    {
                        mbIsRoomChange = true;
                    }
                    roomNum = i;
                }
                //m_Rooms[i].mbIsInRoom = false;
            }
            m_Rooms[roomNum].mbIsInRoom = true;
            m_CurrRoom = m_Rooms[roomNum];
            
            yield return null;
        }
    }

    public void SetTargetPos(Vector2 _targetPos)
    {
        m_TargetPos = _targetPos;
    }

    public void StartCheckInRoom()
    {
        if (m_Coroutine != null)
            StopCoroutine(m_Coroutine);

        m_Coroutine = StartCoroutine(CheckInRoom());
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
