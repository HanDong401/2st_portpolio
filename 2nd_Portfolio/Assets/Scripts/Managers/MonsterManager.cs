using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private AStar m_AStar = new AStar();
    public AStar AStar { get { return m_AStar; } }

    [SerializeField] private Monster[] m_Monsters = null;

    private void Awake()
    {
        m_Monsters = this.GetComponentsInChildren<Monster>();
        m_AStar.InitNode();
        InitMonsterManager();
    }

    private void InitMonsterManager()
    {
        foreach(var monster in m_Monsters)
        {
            monster.AddMonsterEvent(AStar.PathFinding);
        }
    }
}
