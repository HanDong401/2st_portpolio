using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    [SerializeField] private int m_MaxMonsterNum = 0;
    [SerializeField] private int m_MaxRoomMonsterNum = 2;
    [SerializeField] private List<Vector2> m_RoomPoints;
    [SerializeField] private List<Vector2> m_SpawnPoints;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousepos = Input.mousePosition;
            Vector2 randompos = (Random.insideUnitCircle * 3f) + mousepos;
            Debug.Log(randompos);
        }
    }
    public void SetMaxMonsterNum()
    {
        m_MaxMonsterNum = m_RoomPoints.Count + (int)(m_RoomPoints.Count * 0.5f);
    }

    public void SetMaxRoomMonsterNum(int _num)
    {
        m_MaxRoomMonsterNum = _num;
    }

    public void SetSpawnPoint(Vector2 _pos)
    {
        m_RoomPoints.Add(_pos);
    }

    public void SpawnMonster()
    {
        int roomIndex = 0;
        m_SpawnPoints.Add((Random.insideUnitCircle * 3f) + m_RoomPoints[roomIndex]);
    }
}
