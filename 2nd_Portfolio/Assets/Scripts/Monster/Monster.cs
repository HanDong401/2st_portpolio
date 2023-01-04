using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    //Animator m_MonsterAnim = null;
    //public delegate void MonsterEvent();
    //private MonsterEvent m_DieMonster = null;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        Test();
    //    }
    //}

    //public void AddDieMonsterEvent(MonsterEvent _callback)
    //{
    //    m_DieMonster = _callback;
    //}

    //public void Test()
    //{
    //    int num = Random.Range(0, 100);

    //}

    public void Damaged(int _damage)
    {
        m_CurrHp -= _damage;
        if (m_CurrHp <= 0)
        {
            Debug.Log("죽음");
            Destroy(this.gameObject, 2f);
        }
        Debug.Log("현재 체력" + m_CurrHp);
    }
}
