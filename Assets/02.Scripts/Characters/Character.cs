using System;
using System.Collections;
using UnityEngine;
using static Define;

public abstract class Character : MonoBehaviour
{
    private string _name = "Character Name";
    protected int _maxHp = 10;
    protected int _hp = 5;
    protected float _defaultMoveSpeed = 3.0f;
    public float Power { get; set; } = 10;
    public int MoveSpeed { get; set; } = 0;
    public int Defense { get; set; } = 0;
    public int Level { get; set;} = 1;
    public int LevelCnt { get; set; } = 0;
    public float AttackSpeed { get; set; } = 1.0f;
    public float AttackRange { get; set; } = 100;
    private readonly float _protectionTime = 0.1f;
    private float _lastProtectionTime = 0;
    private Bar _hpBar;
    protected SpriteRenderer _renderer;
    public CharacterState state;
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;

    public virtual int MaxHp
    {
        get { return _maxHp; }
        set
        {
            _maxHp = value;
            Hp = _maxHp;
            SetHpUI();
        }
    }

    public virtual int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp > _maxHp)
                _hp = MaxHp;

            SetHpUI();
        }
    }

    protected virtual void Awake()
    {
        _hpBar = GetComponentInChildren<Bar>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        Initialize();
    }
    
    protected void Initialize()
    {
        state = CharacterState.Idle;
        _lastProtectionTime = 0;
        Hp = MaxHp;
        _rigidbody.velocity = Vector2.zero;
        _renderer.flipX = false;
    }

    protected virtual void Move(Vector2 input)
    {
        if (state == CharacterState.Dead) return;
        state = input == Vector2.zero ? CharacterState.Idle : CharacterState.Move;
        Vector2 dir = input.normalized;
        float addSpeed = (100 + MoveSpeed) / 100.0f;
        transform.Translate(dir * (_defaultMoveSpeed * 0.03f * addSpeed));
    }

    protected virtual void Death()
    {
        state = CharacterState.Dead;
        Hp = 0;
    }
    
    protected Vector2 GetDirection(Vector2 a,Vector2 b)
    {
        return (b - a).normalized;
    }
    
    public virtual void ReceiveDamage(float damage, Vector3 knockBackDir = default, bool bCritical = false)
    {
        float ReducePercent = 10 / (10 + (Defense / 1.5f));
        damage *= ReducePercent;
        Hp -= (int)Mathf.Round(damage);
        if (Hp <= 0)
            Death();
        
        StartCoroutine(ReceiveDamageFX());
        if (state is not CharacterState.Move or CharacterState.Idle) return;
        state = CharacterState.Hit;
        KnockBack(knockBackDir);
    }
    
    private IEnumerator ReceiveDamageFX()
    {
        int max = 1;
        _renderer.color = new Color(max, 0, 0);
        for (float i = 0.1f; i <= max; i += 0.1f)
        {
            _renderer.color = new Color(max, i, i);
            yield return new WaitForSeconds(0.01f);
        }
        _renderer.color = Color.white;
        if(state == CharacterState.Attack) yield break;
        
        _rigidbody.velocity= Vector2.zero;
        state = CharacterState.Idle;
    }
    private void KnockBack(Vector3 attackDir)
    {
        _rigidbody.AddForce(attackDir.normalized * 3, ForceMode2D.Impulse);
    }
    protected virtual void SetHpUI()
    {
        if (_hpBar == null)
            return;

        _hpBar.SetBar(MaxHp, Hp);
        _hpBar.SetText(Mathf.FloorToInt(Hp));
    }
}