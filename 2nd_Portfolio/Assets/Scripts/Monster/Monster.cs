using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : Unit
{
    private MonsterBaseState m_CurrState = null;

    protected Animator m_Anim = null;
    public Animator Anim { get { return m_Anim; } }

    protected Rigidbody2D m_Rigid2D = null;
    public Rigidbody2D Rigid2D { get { return m_Rigid2D; } }

    protected Player m_Target;
    public Player Target { get { return m_Target; } set { m_Target = value; } }

    protected SpriteRenderer m_Renderer = null;
    public SpriteRenderer Renderer { get { return m_Renderer; } }

    protected Collider2D m_Collider2D = null;
    public Collider2D Collider { get { return m_Collider2D; } }

    #region 인스펙터
    [Space (10f)]
    [Tooltip("유닛의 이동속도")]
    [SerializeField] protected float m_MoveSpeed = 0f;
    public float MoveSpeed { get { return m_MoveSpeed; } set { m_MoveSpeed = value; } }

    [Header ("몬스터 고유 스테이터스")]
    [Space (10f)]
    [Tooltip ("몬스터가 플레이어를 감지 가능한 거리")]
    [SerializeField] protected float m_DetectRange = 0f;
    public float DetectRange { get { return m_DetectRange; } }

    [Space (10f)]
    [Tooltip("공격 1 의 데미지")]
    [SerializeField] protected int m_Attack1Damage = 0;
    public int Attack1Damage { get { return m_Attack1Damage; } set { m_Attack1Damage = value; } }
    [Tooltip ("공격 1 의 사거리 *존재하지 않으면 0")]
    [SerializeField] protected float m_Attack1Range = 0f;
    public float Attack1Range { get { return m_Attack1Range; } }
    [Tooltip("공격 1 의 딜레이")]
    [SerializeField] protected float m_Attack1Delay = 0f;
    public float Attack1Delay { get { return m_Attack1Delay; } }
    [Space (10f)]
    [Tooltip("공격 2 의 데미지")]
    [SerializeField] protected int m_Attack2Damage = 0;
    public int Attack2Damage { get { return m_Attack2Damage; } set { m_Attack2Damage = value; } }
    [Tooltip("공격 2 의 사거리 *존재하지 않으면 0")]
    [SerializeField] protected float m_Attack2Range = 0f;
    public float Attack2Range { get { return m_Attack2Range; } }
    [Tooltip("공격 2 의 딜레이")]
    [SerializeField] protected float m_Attack2Delay = 0f;
    public float Attack2Delay { get { return m_Attack2Delay; } }
    [Space(10f)]
    [Tooltip("공격 3 의 데미지")]
    [SerializeField] protected int m_Attack3Damage = 0;
    public int Attack3Damage { get { return m_Attack3Damage; } set { m_Attack3Damage = value; } }
    [Tooltip("공격 3 의 사거리 *존재하지 않으면 0")]
    [SerializeField] protected float m_Attack3Range = 0f;
    public float Attack3Range { get { return m_Attack3Range; } }
    [Tooltip("공격 3 의 딜레이")]
    [SerializeField] protected float m_Attack3Delay = 0f;
    public float Attack3Delay { get { return m_Attack3Delay; } }
    [Space(10f)]
    [Tooltip("어빌리티 의 딜레이")]
    [SerializeField] protected float m_AbilityDelay = 0f;
    public float AbilityDelay { get { return m_AbilityDelay; } }

    [Space (10f)]
    [Tooltip("몬스터가 감지할 대상의 레이어 설정")]
    [SerializeField] protected LayerMask m_TargetLayer;
    public LayerMask TargetLayer { get { return m_TargetLayer; } }
    [Tooltip("몬스터가 무시할 대상의 레이어 설정")]
    [SerializeField] protected LayerMask m_IgnoreLayer;
    public LayerMask IgnoreLayer { get { return m_IgnoreLayer; } }
    [Space (10f)]
    [Tooltip("어빌리티 발동 가능 유무")]
    [SerializeField] protected bool mbIsCanAbility = true;
    public bool IsCanAbility { get { return mbIsCanAbility; } set { mbIsCanAbility = value; } }
    [Tooltip("공격1 발동 가능 유무")]
    [SerializeField] protected bool mbIsCanAttack1 = false;
    public bool IsCanAttack1 { get { return mbIsCanAttack1; } set { mbIsCanAttack1 = value; } }
    [Tooltip("공격2 발동 가능 유무")]
    [SerializeField] protected bool mbIsCanAttack2 = false;
    public bool IsCanAttack2 { get { return mbIsCanAttack2; } set { mbIsCanAttack2 = value; } }
    [Tooltip("공격3 발동 가능 유무")]
    [SerializeField] protected bool mbIsCanAttack3 = false;
    public bool IsCanAttack3 { get { return mbIsCanAttack3; } set { mbIsCanAttack3 = value; } }
    [Space(10f)]
    [Header("프리팹")]
    [Space(10f)]
    [Tooltip("공격 볌위 표시 프리팹")]
    [SerializeField] protected AttackRange m_AttackRangePrefab = null;
    public AttackRange AttackRangePrefab { get { return m_AttackRangePrefab; } }
    #endregion

    public delegate List<Node> MonsterEvent(Vector2 _startPos, Vector2 _endPos);
    public MonsterEvent m_MonsterEvent = null;
    public delegate Monster MonsterSummonEvent(string _monster, Vector2 _pos);
    public MonsterSummonEvent m_MonsterSummonEvent = null;
    public delegate void RemoveMonsterListEvent(Monster _monster);
    public RemoveMonsterListEvent m_RemoveMonster = null; 

    private void Awake()
    {
        InitMonster();
        SubAwake();
    }

    private void Start()
    {
        StartCoroutine(RunUpdateCoroutine());
    }

    public void InitMonster()
    {
        m_Renderer = this.GetComponentInChildren<SpriteRenderer>();
        m_Anim = this.GetComponentInChildren<Animator>();
        m_Rigid2D = this.GetComponent<Rigidbody2D>();
        m_Collider2D = this.GetComponent<Collider2D>();
        ChangeState("Idle");
        SetCurrHp(m_MaxHp);
    }

    public abstract bool SubCheckState();
    public abstract void Attack1();
    public abstract void Attack2();
    public abstract void Attack3();
    public abstract void Ability();
    public abstract void Death();
    public abstract void SubAwake();

    public void ChangeState(string _state)
    {
        switch(_state)
        {
            case "Idle":
                m_CurrState = new MonsterIdleState(this);
                break;
            case "Run":
                m_CurrState = new MonsterRunState(this);
                break;
            case "Hit":
                m_CurrState = new MonsterHitState(this);
                break;
            case "Attack1":
                m_CurrState = new MonsterAttack1State(this);
                break;
            case "Attack2":
                m_CurrState = new MonsterAttack2State(this);
                break;
            case "Attack3":
                m_CurrState = new MonsterAttack3State(this);
                break;
            case "Ability":
                m_CurrState = new MonsterAbilityState(this);
                break;
            case "Death":
                m_CurrState = new MonsterDeathState(this);
                break;
        }
    }


    public void AddMonsterEvent(MonsterEvent _callback)
    {
        m_MonsterEvent = _callback;
    }

    public void AddMonsterSummonEvent(MonsterSummonEvent _callback)
    {
        m_MonsterSummonEvent = _callback;
    }

    public void AddRemoveMonsterEvent(RemoveMonsterListEvent _callback)
    {
        m_RemoveMonster = _callback;
    }

    public void OnRemoveMonster()
    {
        if (m_RemoveMonster != null)
            m_RemoveMonster(this);
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Vector2 GetTargetPosition()
    {
        return Target.GetPosition();
    }

    public void OnDamaged(int _damage)
    {
        ChangeState("Hit");
        OnDamage(_damage);
        if (m_CurrHp <= 0)
        {
            Debug.Log("죽음");
            Destroy(this.gameObject, 2f);
        }
    }

    public void StartDelay(string _state, float _delay)
    {
        StartCoroutine(ChangeStateDelayCoroutine(_state, _delay));
    }

    public void AbilityCoolTime(float _delay)
    {
        StartCoroutine(AbilityCoolTimeCoroutine(_delay));
    }

    public void Attack1CoolTime(float _delay)
    {
        StartCoroutine(Attack1CoolTimeCoroutine(_delay));
    }

    public void Attack2CoolTime(float _delay)
    {
        StartCoroutine(Attack2CoolTimeCoroutine(_delay));
    }

    public void Attack3CoolTime(float _delay)
    {
        StartCoroutine(Attack3CoolTimeCoroutine(_delay));
    }

    IEnumerator RunUpdateCoroutine()
    {
        while(true)
        {
            m_CurrState.UpdateState();
            m_CurrState.CheckState();
            yield return null;
        }
    }

    IEnumerator ChangeStateDelayCoroutine(string _state, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        ChangeState(_state);
    }

    IEnumerator AbilityCoolTimeCoroutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        mbIsCanAbility = true;
    }
    IEnumerator Attack1CoolTimeCoroutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        mbIsCanAttack1 = true;
    }
    IEnumerator Attack2CoolTimeCoroutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        mbIsCanAttack2 = true;
    }
    IEnumerator Attack3CoolTimeCoroutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        mbIsCanAttack3 = true;
    }

}
