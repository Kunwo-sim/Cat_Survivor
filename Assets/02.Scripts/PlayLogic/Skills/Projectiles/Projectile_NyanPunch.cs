using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_NyanPunch : Projectile
{
    public void Initialize(Vector2 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType, float alpha)
    {
        base.Initialize(spawnPos, spawnRot, damage, activeTime, poolType);
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }
    protected override IEnumerator Move()
    {
        yield break;
    }
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().ReceiveDamage(damage, transform.right);
        }
    }
}