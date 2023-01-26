using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using static Define;

public abstract class Enemy : Character
{
    protected Player _player;
    private EPoolObjectType _poolType;
    
    private Targeting _targeting = new Targeting();
    private EnemyProjectile cloneProjectile;
    private DamagePopup _damagePopup;

    public void Initialize(int hp, int power, float moveSpeed, int level, EPoolObjectType poolType)
    {
        base.Initialize();
        Hp = MaxHp = hp;
        Power = power;
        MoveSpeed = moveSpeed;
        Level = level;
        _poolType = poolType;
        StartCoroutine(SpawnFadeIn());
    }

    IEnumerator SpawnFadeIn()
    {
        state = CharacterState.Hit;
        _collider.enabled = false;
        
        int max = 1;
        _renderer.color = Color.black;
        for (float i = 0f; i <= max; i += 0.01f)
        {
            _renderer.color = new Color(i, i, i);
            yield return new WaitForSeconds(0.01f);
        }
        _renderer.color = Color.white;
        _collider.enabled = true;
        state = CharacterState.Idle;

        Routine();
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
        if (state is CharacterState.Move)
            base.Move(input);
    }

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _damagePopup = Resources.Load<DamagePopup>("Prefabs/DamagePopup");
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
        DamagePopup d = Instantiate(_damagePopup);
        d.Setup((int)damage, transform.position);
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.ReceiveDamage(Power * Time.fixedDeltaTime * 10);
        }
    }
    
    protected abstract void Routine();
    protected IEnumerator Routine_Move()
    {
        state = CharacterState.Move;
        yield return new WaitForSeconds(3f);
        Routine();
    }
    protected IEnumerator Routine_Shot()
    {
        state = CharacterState.Attack;
        
        int max = 1;
        _renderer.color = new Color(max, max, max);
        for (float i = 0.1f; i <= max; i += 0.01f)
        {
            _renderer.color = new Color(max, max-i, max-i);
            yield return new WaitForSeconds(0.01f);
        }
        _renderer.color = Color.white;
        
        var dir = GetDirection(transform.position, _player.transform.position);
        var pos = transform.position;
        var rot = _targeting.GetToNearRotate(dir);
        var poolType = EPoolObjectType.EnemyProjectile;
        
        cloneProjectile = ObjectPoolManager.GetObject(poolType).GetComponent<EnemyProjectile>();
        cloneProjectile.Initialize(pos, rot, (int)Power, 3, poolType);
        
        yield return new WaitForSeconds(0.3f);
        Routine();
    }
    protected IEnumerator Routine_Dash()
    {
        state = CharacterState.Attack;
        Vector3 dashDir = GetDirection(transform.position, _player.transform.position);
        
        int max = 1;
        _renderer.color = new Color(max, max, max);
        for (float i = 0.1f; i <= max; i += 0.02f)
        {
            _renderer.color = new Color(max, max-i, max-i);
            yield return new WaitForSeconds(0.01f);
        }
        _renderer.color = Color.white;

        _collider.isTrigger = true;
        float dashSpeed = 15;
        _rigidbody.AddForce(dashDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        _rigidbody.velocity = Vector2.zero;
        _collider.isTrigger = false;
        
        Routine();
    }
}