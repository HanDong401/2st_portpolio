using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    [SerializeField] private int m_MaxMonsterNum = 0;
    [SerializeField] private int m_MaxRoomMonsterNum = 2;
    [SerializeField] private List<Vector2Int> m_RoomPoints;
    [SerializeField] private List<Vector2> m_SpawnPoints;
    public delegate void SpawnPointEvent(Vector2[] _pos);
    private SpawnPointEvent m_SpawnPointEvent = null;


    public void MonsterSpawnPointAwake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetMaxMonsterNum()
    {
        m_MaxMonsterNum = m_RoomPoints.Count + (int)(m_RoomPoints.Count * 0.5f);
    }

    public void SetMaxRoomMonsterNum(int _num)
    {
        m_MaxRoomMonsterNum = _num;
    }

    public void SetRoomPoint(Vector2Int[] _pos)
    {
        m_RoomPoints.Clear();
        if (_pos == null)
        {
            m_SpawnPoints.Clear();
            return;
        }
        foreach(Vector2Int p in _pos)
        {
            m_RoomPoints.Add(p);
        }
        SetSpawnPoint();
    }

    public void SetSpawnPoint()
    {
        SetMaxMonsterNum();
        m_SpawnPoints.Clear();
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
        m_SpawnPointEvent(m_SpawnPoints.ToArray());
    }

    public Vector2[] GetSpawnPoint()
    {
        return m_SpawnPoints.ToArray();
    }

    public void AddSpawnPointEvent(SpawnPointEvent _callback)
    {
        m_SpawnPointEvent = _callback;
    }
}
