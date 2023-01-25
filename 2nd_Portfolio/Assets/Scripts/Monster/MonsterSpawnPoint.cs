using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    [SerializeField] private int m_MaxMonsterNum = 0;
    [SerializeField] private int m_MaxRoomMonsterNum = 2;
    [SerializeField] private List<Vector2> m_RoomPoints;
    [SerializeField] private List<Vector2> m_SpawnPoints;

    public void SetMaxMonsterNum()
    {
        m_MaxMonsterNum = m_RoomPoints.Count + (int)(m_RoomPoints.Count * 0.5f);
    }

    public void SetMaxRoomMonsterNum(int _num)
    {
        m_MaxRoomMonsterNum = _num;
    }

    public void SetRoomPoint(Vector2 _pos)
    {
        m_RoomPoints.Add(_pos);
    }

    public void SetSpawnPoint()
    {
        SetMaxMonsterNum();
        int roomIndex = 0;
        while(m_MaxMonsterNum > 0)
        {
            int roomMonsterNum = Random.Range(0, m_MaxRoomMonsterNum);
            for (int i = 0; i < roomMonsterNum; ++i)
            {
                m_SpawnPoints.Add((Random.insideUnitCircle * 1f) + m_RoomPoints[roomIndex]);
                --m_MaxMonsterNum;
            }
            ++roomIndex;
            if (roomIndex >= m_RoomPoints.Count)
                roomIndex = 0;
        }
    }
}
