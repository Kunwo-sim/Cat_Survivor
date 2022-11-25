using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using static Define;

public class Enemy : Character
{
    private Player _player;
    private EPoolObjectType _poolType;

    public void Initialize(int hp, int power, float moveSpeed, int level, EPoolObjectType poolType)
    {
        Initialize();
        Hp = MaxHp = hp;
        Power = power;
        MoveSpeed = moveSpeed;
        Level = level;
        _poolType = poolType;
    }

    private void FixedUpdate()
    {
        Move(GetDirection(transform.position, _player.transform.position));
    }

    private void CreatExpObject()
    {
        ExpObject expObject = ObjectPoolManager.GetObject(EPoolObjectType.ExpObject).GetComponent<ExpObject>();
        expObject.Initialize(Level, transform.position);
    }

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        // Test code
        MoveSpeed = Random.Range(1.0f, 4.0f);
    }

    protected override void Death()
    {
        base.Death();
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