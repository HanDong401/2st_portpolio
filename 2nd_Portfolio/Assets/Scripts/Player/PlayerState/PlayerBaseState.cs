using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public PlayerBaseState(Player _player)
    {
        m_Player = _player;
        if (m_CurrState != null)
            m_CurrState.ExitState();
        m_CurrState = this;
        m_CurrState.EnterState();
    }
    protected static PlayerBaseState m_CurrState = null;

    // EnterState �� ExitState �� �ڵ����� �����ϰ� �ϱ����ؼ���
    // �� State�� �����ڿ� �� State�� Exit�� �� State�� Enter�� �����ų �ʿ䰡 �ִ�
    protected Player m_Player = null;


    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckState();
}
