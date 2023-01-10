using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState : MonoBehaviour
{
    //private MonsterIdleState m_MonsterIdleState = null;
    //private MonsterRunState m_MonsterRunState = null;
    //private MonsterHitState m_MonsterHitState = null;
    //private MonsterDeathState m_MonsterDeathState = null;
    //private MonsterAbilityState m_MonsterAbilityState = null;
    //private MonsterAttack1State m_MonsterAttack1State = null;
    //private MonsterAttack2State m_MonsterAttack2State = null;
    //private MonsterAttack3State m_MonsterAttack3State = null;

    [SerializeField] private MonsterBaseState m_MonsterIdleState = null;
    public MonsterBaseState MonsterIdle
    {
        get { return m_MonsterIdleState; }
        set
        {
            m_MonsterIdleState = (MonsterBaseState)value;
        }
    }
    [SerializeField] private MonsterBaseState m_MonsterRunState = null;

    //private MonsterBaseState m_CurrState = null;

    //public void ChangeState(string _state)
    //{
    //    MonsterIdleState idle = new MonsterIdleState();
    //    MonsterBaseState monster = m_MonsterIdleState;
    //    switch(_state)
    //    {
    //        case "Idle":
    //            m_CurrState = m_MonsterIdleState;
    //            break;
    //        case "Run":
    //            break;
    //        case "Hit":
    //            break;
    //    }
    //}



    public virtual void EnterState()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {

    }
}
