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
    [SerializeField] private Rat m_Rat = null;
    [SerializeField] private Pebble m_Pebble = null;
    [SerializeField] private List<Monster> m_MonsterList = new List<Monster>();
    [SerializeField] private Vector2[] m_SpawnPoint = null;

    public void MonsterManagerAwake()
    {
        DontDestroyOnLoad(this.gameObject);
        m_MonsterList.Clear();
        //m_MonsterList = this.GetComponentsInChildren<Monster>().ToList();
        //m_AStar.InitNode();
        //InitMonsterManager();
    }

    public void MonsterManagerStart()
    {
        //m_AStar.InitNode();
        //if (m_SpawnPoint == null)
        //    m_SpawnPoint = GameObject.FindObjectOfType<SpawnPoint>();
        //if (m_SpawnPoint != null)
        //{
        //    m_SpawnPoint.AddMonsterSummonEvent(SummonRandomMonster);
        //    m_SpawnPoint.SpawnPointAwake();
        //}
    }

    private void InitMonsterManager()
    {
        for(int i = 0; i < m_MonsterList.Count; ++i)
        {
            m_MonsterList[i].AddMonsterEvent(AStar.PathFinding);
            m_MonsterList[i].AddMonsterSummonEvent(SummonMonster);
        }
    }

    public Monster SummonMonster(string _monster, Vector2 _pos)
    {
        Monster monster = Instantiate(SelectMonster(_monster));
        monster.AddMonsterEvent(AStar.PathFinding);
        monster.AddRemoveMonsterEvent(RemoveMonster);
        monster.AddMonsterSummonEvent(SummonMonster);
        m_MonsterList.Add(monster);
        monster.transform.SetParent(this.transform, false);
        monster.transform.position = _pos;
        return monster;
    }

    public Monster SummonRandomMonster(Vector2 _pos)
    {
        Monster monster = Instantiate(RandomSelectMonster());
        monster.AddMonsterEvent(AStar.PathFinding);
        monster.AddRemoveMonsterEvent(RemoveMonster);
        monster.AddMonsterSummonEvent(SummonMonster);
        m_MonsterList.Add(monster);
        monster.transform.SetParent(this.transform, false);
        monster.transform.position = _pos;
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
            case "Rat":
                return m_Rat;
            case "Pebble":
                return m_Pebble;
        }
        return null;
    }

    public Monster RandomSelectMonster()
    {
        int index = Random.Range(0, 3);

        switch(index)
        {
            case 0:
                return m_Bat;
            case 1:
                return m_Rat;
            case 2:
                return m_Slime;
        }
        return null;
    }

    private void AddMonster(Monster _monster)
    {
        m_MonsterList.Add(_monster);
        _monster.AddMonsterEvent(AStar.PathFinding);
    }

    public void RemoveMonster(Monster _monster)
    {
        m_MonsterList.Remove(_monster);
    }

    public void RemoveAllMonster()
    {
        foreach(Monster _monster in m_MonsterList)
        {
            Destroy(_monster.gameObject);
        }
        m_MonsterList.Clear();
    }

    public int GetMonsterListCount()
    {
        return m_MonsterList.Count;
    }

    public void SetSpawnPoint(Vector2[] _pos)
    {
        m_SpawnPoint = _pos;
    }
}
