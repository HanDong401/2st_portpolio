using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public MonsterManager monsterManager;
    public Monster Monster;

    private void Awake()
    {
        monsterManager.MonsterManagerAwake();
        Vector2[] vectors = new Vector2[] { Vector2.zero, Vector2.right };
        monsterManager.SetSpawnPoint(vectors);
        monsterManager.SummonRandomMonster();
    }
}
