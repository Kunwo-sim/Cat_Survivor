using System;
using System.Collections;
using UnityEngine;
using static Define;

public abstract class Character : MonoBehaviour
{
    private string _name = "Character Name";
    protected float MaxHp = 10;
    protected float Hp = 10;
    protected float Power = 1;
    protected float MoveSpeed = 3.0f;
    protected int Level = 1;
    private readonly float _protectionTime = 0.1f;
    private float _lastProtectionTime = 0;
    private Bar _hpBar;
    protected SpriteRenderer _renderer;
    public CharacterState state;
    private Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        _hpBar = GetComponentInChildren<Bar>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
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
    }

    protected void Move(Vector2 input)
    {
        if (state == CharacterState.Dead) return;
        state = input == Vector2.zero ? CharacterState.Idle : CharacterState.Move;
        Vector2 dir = input.normalized;
        transform.Translate(dir * (MoveSpeed * 0.03f));
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
    
    public virtual void ReceiveDamage(float damage, Vector3 knockBackDir = default)
    {
        Hp -= damage;
        if (Hp <= 0)
            Death();
        
        if (state is CharacterState.Hit or CharacterState.Dead) return;
        state = CharacterState.Hit;
        StartCoroutine(ReceiveDamageFX());
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
        _rigidbody.velocity= Vector2.zero;
        state = CharacterState.Idle;
    }
    private void KnockBack(Vector3 attackDir)
    {
        _rigidbody.AddForce(attackDir.normalized * 3, ForceMode2D.Impulse);
    }
    protected void SetHpUI()
    {
        _hpBar.SetBar(MaxHp, Hp);
        _hpBar.SetText(Mathf.FloorToInt(Hp));
    }
}