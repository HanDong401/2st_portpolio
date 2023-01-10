using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    private MonsterBaseState m_CurrState = null;

    protected Animator m_Anim = null;
    public Animator Anim { get { return m_Anim; } }

    protected Rigidbody2D m_Rigid2D = null;
    public Rigidbody2D Rigid2D { get { return m_Rigid2D; } }

    protected Transform m_Target;
    public Transform Target { get { return m_Target; } set { m_Target = value; } }

    protected SpriteRenderer m_Renderer = null;
    public SpriteRenderer Renderer { get { return m_Renderer; } }

    [SerializeField] protected LayerMask m_TargetLayer;
    public LayerMask TargetLayer { get { return m_TargetLayer; } }

    [SerializeField] protected float m_DetectRange = 0f;
    public float DetectRange { get { return m_DetectRange; } }

    [SerializeField] protected float m_Attack1Range = 0f;
    public float Attack1Range { get { return m_Attack1Range; } }

    [SerializeField] protected float m_Attack2Range = 0f;
    public float Attack2Range { get { return m_Attack2Range; } }

    [SerializeField] protected float m_Attack3Range = 0f;
    public float Attack3Range { get { return m_Attack3Range; } }

    [SerializeField] protected float m_MoveSpeed = 0f;
    public float MoveSpeed { get { return m_MoveSpeed; } set { m_MoveSpeed = value; } }

    [SerializeField] protected int m_Damage = 0;
    public int Damage { get { return m_Damage; } set { m_Damage = value; } }

    public delegate List<Node> MonsterEvent(Vector2 _startPos, Vector2 _endPos);
    public MonsterEvent m_MonsterEvent = null;

    public List<Node> PathList;

    private void Awake()
    {
        m_Anim = this.GetComponent<Animator>();
        m_Rigid2D = this.GetComponent<Rigidbody2D>();
        m_Renderer = this.GetComponent<SpriteRenderer>();
        ChangeState("Idle");
        InitMonster();
    }

    private void Update()
    {
        m_CurrState.UpdateState();
        m_CurrState.CheckState();
    }

    protected void InitMonster()
    {
        SubExcute();
    }

    protected virtual void SubExcute()
    {

    }

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

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void OnDamaged(int _damage)
    {
        ChangeState("Hit");
        OnDamage(_damage);
        if (m_CurrHp <= 0)
        {
            Debug.Log("Á×À½");
            Destroy(this.gameObject, 2f);
        }
    }
}
