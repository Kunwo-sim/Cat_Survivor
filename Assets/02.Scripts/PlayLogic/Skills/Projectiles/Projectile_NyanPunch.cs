using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_NyanPunch : Projectile
{

    protected override IEnumerator Move()
    {
        yield return null;
    }

    public override void Initialize(Vector2 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType)
    {
        base.Initialize(spawnPos, spawnRot, damage, activeTime, poolType);
        // Test
        gameObject.GetComponentsInChildren<Transform>()[1].localPosition += new Vector3(Random.Range(-0.7f, 0.7f), 0, 0);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().ReceiveDamage(damage, transform.up);
        }
    }
}