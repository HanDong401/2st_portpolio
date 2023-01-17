using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    Monster eMonster;
    private AStar m_AStar = new AStar();
    public AStar AStar { get { return m_AStar; } }
    [SerializeField] private Bat m_Bat = null;
    [SerializeField] private Slime m_Slime = null;
    [SerializeField] private Slime m_MiniSlime = null;
    [SerializeField] private Golem1 m_Golem1 = null;
    [SerializeField] private Golem2 m_Golem2 = null;
    [SerializeField] private Golem3 m_Golem3 = null;
    [SerializeField] private List<Monster> m_MonsterList = new List<Monster>();

    private void Awake()
    {
        m_MonsterList = this.GetComponentsInChildren<Monster>().ToList();
        m_AStar.InitNode();
        InitMonsterManager();
    }

    private void InitMonsterManager()
    {
        for(int i = 0; i < m_MonsterList.Count; ++i)
        {
            m_MonsterList[i].AddMonsterEvent(AStar.PathFinding);
            m_MonsterList[i].AddMonsterSummonEvent(SummonMonster);
        }
    }

    public Monster SummonMonster(string _monster, Vector2 _Pos)
    {
        Monster monster = Instantiate(SelectMonster(_monster));
        monster.AddMonsterEvent(AStar.PathFinding);
        m_MonsterList.Add(monster);
        monster.transform.SetParent(this.transform, false);
        monster.transform.position = _Pos;
        return monster;
    }

    private Monster SelectMonster(string _monster)
    {
        switch(_monster)
        {
            case "Bat":
                return m_Bat;
            case "Slime":
                return m_Slime;
            case "MiniSlime":
                return m_MiniSlime;
            case "Golem1":
                return m_Golem1;
            case "Golem2":
                return m_Golem2;
            case "Golem3":
                return m_Golem3;
                
        }
        return null;
    }

    private void AddMonster(Monster _monster)
    {
        m_MonsterList.Add(_monster);
        _monster.AddMonsterEvent(AStar.PathFinding);
    }
}
