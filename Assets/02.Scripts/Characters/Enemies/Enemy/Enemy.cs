using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using static Define;

public abstract class Enemy : Character
{
    protected Player _player;
    protected EPoolObjectType _poolType;

    protected EnemyProjectile _cloneProjectile;
    protected Targeting _targeting = new Targeting();
    protected DamagePopup _damagePopup;
    private Vector2 _direction;

    public void Initialize(int hp, float power, float moveSpeed, int level, EPoolObjectType poolType)
    {
        base.Initialize();
        float waveValue = WaveManager.Instance._waveStep * 0.6f;
        hp *= Mathf.RoundToInt(waveValue);
        Hp = MaxHp = hp;
        Power = power *= waveValue;
        _defaultMoveSpeed = moveSpeed;
        Level = level;
        _poolType = poolType;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
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
        _direction = GetDirection(transform.position, _player.transform.position);
        Move(_direction);
    }

    protected virtual void FlipXRenderer(Vector2 direction)
    {
        _renderer.flipX = direction.x < 0;
    }

    private void CreatExpObject()
    {
        ExpObject expObject = ObjectPoolManager.GetObject(EPoolObjectType.ExpObject).GetComponent<ExpObject>();
        expObject.Initialize(1, Level, transform.position);
    }

    protected override void Move(Vector2 input)
    {
        if (state is CharacterState.Move or CharacterState.Hit)
        {
            base.Move(input);
            FlipXRenderer(_direction);
        }
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
        SoundManager.Instance.PlaySFXSound("Enemy_Death");
        StartCoroutine(DeathFadeOut());
    }

    public override void ReceiveDamage(float damage, Vector3 knockBackDir = default, bool bCiritical = false)
    {
        DamagePopup d = Instantiate(_damagePopup);
        d.Setup((int)damage, transform.position, bCiritical);
        SoundManager.Instance.PlaySFXSound("SkillHit");
        base.ReceiveDamage(damage, knockBackDir);
    }

    protected virtual void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            _player.ReceiveDamage(Power);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.ReceiveDamage(Power*1.5f);
        }
    }

    protected abstract void Routine();
    protected virtual IEnumerator Routine_Move()
    {
        state = CharacterState.Move;
        yield return new WaitForSeconds(2f);
        Routine();
    }
    protected virtual IEnumerator Routine_Shot()
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
        
        _cloneProjectile = ObjectPoolManager.GetObject(poolType).GetComponent<EnemyProjectile>();
        _cloneProjectile.Initialize(pos, rot, (int)Power, 3, poolType);
        
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(Routine_Move());
    }
    protected virtual IEnumerator Routine_Dash()
    {
        state = CharacterState.Attack;
        
        int max = 1;
        _renderer.color = new Color(max, max, max);
        for (float i = 0.0f; i <= max; i += 0.02f)
        {
            _renderer.color = new Color(max, max-i, max-i);
            yield return new WaitForSeconds(0.01f);
        }
        _renderer.color = Color.white;
        Vector3 dashDir = GetDirection(transform.position, _player.transform.position);
        _collider.isTrigger = true;
        float dashSpeed = 15;
        _rigidbody.AddForce(dashDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        _rigidbody.velocity = Vector2.zero;
        _collider.isTrigger = false;
        
        StartCoroutine(Routine_Move());
    }
    protected virtual IEnumerator  DeathFadeOut()
    {
        _collider.enabled = false;
        
        float max = 0.95f;
        for (float i = 0f; i <= max; i += 0.02f)
        {
            float value = max-i;
            _renderer.color = new Color(value, value, value);
            transform.Rotate(0,0,5);
            transform.localScale = new Vector3(value, value, value);
            yield return new WaitForSeconds(0.01f);
        }
        ObjectPoolManager.ReturnObject(gameObject, _poolType);
    }
}