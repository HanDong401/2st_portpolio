using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Point[] m_Points = null;
    public delegate void MonsterSummonEvent(Vector2 _pos);
    private MonsterSummonEvent m_MonsterSummonEvent = null;

    public void SpawnPointAwake()
    {
        m_Points = this.GetComponentsInChildren<Point>();
        if (m_Points.Length > 0)
        {
            foreach(Point point in m_Points)
            {
                m_MonsterSummonEvent(point.GetPosition());
            }
        }
    }

    public void AddMonsterSummonEvent(MonsterSummonEvent _callback)
    {
        m_MonsterSummonEvent = _callback;
    }
}
