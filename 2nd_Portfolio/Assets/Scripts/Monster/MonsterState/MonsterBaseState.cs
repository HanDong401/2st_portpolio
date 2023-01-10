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

    // EnterState �� ExitState �� �ڵ����� �����ϰ� �ϱ����ؼ���
    // �� State�� �����ڿ� �� State�� Exit�� �� State�� Enter�� �����ų �ʿ䰡 �ִ�
    protected Monster m_Monster = null;


    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckState();
}
