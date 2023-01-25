using System.Collections;
using UnityEngine;
public class EnemyProjectile : MonoBehaviour
{
    protected int damage;
    protected float speed;

    protected Rigidbody2D rigidbody2D;
    protected SpriteRenderer spriteRenderer;
    private EPoolObjectType _poolType = EPoolObjectType.EnemyProjectile;

    protected virtual void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().ReceiveDamage(damage, transform.right);
            CancelInvoke(nameof(Delete));
            Delete();
        }
    }
    protected virtual void Delete()
    {
        ObjectPoolManager.ReturnObject(gameObject, _poolType);
    }

    public virtual void Initialize(Vector3 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType)
    {
        transform.SetPositionAndRotation(spawnPos, spawnRot);
        rigidbody2D.velocity = Vector2.zero;
        this.damage = damage;
        _poolType = poolType;
        Invoke(nameof(Delete), activeTime);
        Move();
    }

    
    void Move()
    {
        speed = 10;
        Vector2 force = transform.right.normalized * speed;
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }
}