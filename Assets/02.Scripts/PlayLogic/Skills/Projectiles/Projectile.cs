using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Define.ProjectileType _projectileType;
    protected int damage = 0;
    protected float speed;
    bool bCritical = false;

    protected Player _player;
    protected Rigidbody2D rigidbody2D;
    protected SpriteRenderer spriteRenderer;
    private EPoolObjectType _poolType;

    protected virtual void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (_projectileType == Define.ProjectileType.Melee)
            {
                damage = GetMeleeDamage(damage);
            }
            else if (_projectileType == Define.ProjectileType.Range)
            {
                damage = GetRangeDamage(damage);
            }

            if(CriticalCheck())
            {
                bCritical = true;
                damage *= 2;
            }

            col.GetComponent<Enemy>().ReceiveDamage(damage, transform.right, bCritical);

            if (_projectileType == Define.ProjectileType.Melee)
                return;

            CancelInvoke(nameof(Delete));
            Delete();
        }
    }
    protected virtual void Delete()
    {
        StopCoroutine(Move());
        ObjectPoolManager.ReturnObject(gameObject, _poolType);
    }

    public virtual void Initialize(Vector3 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType)
    {
        transform.SetPositionAndRotation(spawnPos, spawnRot);
        rigidbody2D.velocity = Vector2.zero;
        this.damage = damage;
        _poolType = poolType;
        Invoke(nameof(Delete), activeTime);
        StartCoroutine(Move());
    }

    protected abstract IEnumerator Move();
    protected int GetMeleeDamage(int damage)
    {
        damage = (damage + _player.MeleeAttack) * (100 + _player.Attack) / 100;
        return damage;
    }
    protected int GetRangeDamage(int damage)
    {
        damage = (damage + _player.RangeAttack) * (100 + _player.Attack) / 100;
        return damage;
    }
    protected bool CriticalCheck()
    {
        if (_player.Critical >= UnityEngine.Random.Range(0, 101))
            return true;
        else
            return false;
    }
}