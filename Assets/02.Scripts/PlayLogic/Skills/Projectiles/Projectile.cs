using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected int damage;
    protected float speed;

    protected Rigidbody2D rigidbody2D;
    private EPoolObjectType _poolType;

    protected virtual void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().ReceiveDamage(damage, transform.right);
            CancelInvoke(nameof(Delete));
            Delete();
        }
    }

    protected virtual void Delete()
    {
        StopCoroutine(Move());
        ObjectPoolManager.ReturnObject(gameObject, _poolType);
    }

    public virtual void Initialize(Vector2 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType)
    {
        transform.SetPositionAndRotation(spawnPos, spawnRot);
        rigidbody2D.velocity = Vector2.zero;
        this.damage = damage;
        _poolType = poolType;
        Invoke(nameof(Delete), activeTime);
        StartCoroutine(Move());
    }

    protected abstract IEnumerator Move();
}