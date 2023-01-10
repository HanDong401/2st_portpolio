using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseState
{
    public MonsterBaseState(Monster _monster)
    {
        m_Monster = _monster;
        if (m_CurrState != null)
            m_CurrState.ExitState();
        m_CurrState = this;
        m_CurrState.EnterState();
}
    protected static MonsterBaseState m_CurrState = null;

    // EnterState 와 ExitState 를 자동으로 실행하게 하기위해서는
    // 각 State의 생성자에 전 State의 Exit와 현 State의 Enter를 실행시킬 필요가 있다
    protected Monster m_Monster = null;


    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckState();
}
