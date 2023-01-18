using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_NyanPunch : Projectile
{
    private SpriteRenderer _spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override IEnumerator Move()
    {
        yield return null;
    }

    public void Initialize(Vector2 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType, float alpha)
    {
        base.Initialize(spawnPos, spawnRot, damage, activeTime, poolType);
        _spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
        
        // Test
        // gameObject.transform.localPosition += new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().ReceiveDamage(damage, transform.up);
        }
    }
}