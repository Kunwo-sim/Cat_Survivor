using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using static Define;

public abstract class Enemy : Character
{
    protected Player _player;
    private EPoolObjectType _poolType;

    public void Initialize(int hp, int power, float moveSpeed, int level, EPoolObjectType poolType)
    {
        Initialize();
        Hp = MaxHp = hp;
        Power = power;
        MoveSpeed = moveSpeed;
        Level = level;
        _poolType = poolType;
        _renderer.color = Color.white;
    }

    private void FixedUpdate()
    {
        Vector2 direction = GetDirection(transform.position, _player.transform.position);
        Move(direction);
        _renderer.flipX = direction.x < 0 ? true : false;
    }

    private void CreatExpObject()
    {
        ExpObject expObject = ObjectPoolManager.GetObject(EPoolObjectType.ExpObject).GetComponent<ExpObject>();
        expObject.Initialize(Level, transform.position);
    }

    protected override void Move(Vector2 input)
    {
        if (state is CharacterState.Hit or CharacterState.Attack) return;
        base.Move(input);
    }

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    protected override void Death()
    {
        base.Death();
        StopAllCoroutines();
        CreatExpObject();
        ObjectPoolManager.ReturnObject(gameObject, _poolType);
    }

    public override void ReceiveDamage(float damage, Vector3 knockBackDir = default)
    {
        SoundManager.Instance.PlaySFXSound("SkillHit");
        base.ReceiveDamage(damage, knockBackDir);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            _player.ReceiveDamage(Power * Time.fixedDeltaTime * 10);
        }
    }
}