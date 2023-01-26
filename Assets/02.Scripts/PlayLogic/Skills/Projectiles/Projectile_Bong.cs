using System;
using System.Collections;
using UnityEngine;
public class Projectile_Bong : Projectile
{

    private Transform _holderTrans;
    public void Initialize(Vector2 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType, Transform holderTrans)
    {
        base.Initialize(spawnPos, spawnRot, damage, activeTime, poolType);
        _holderTrans = holderTrans;

    }
    private void FixedUpdate()
    {
        var dir = _holderTrans.position - transform.position;
        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    protected override IEnumerator Move()
    {
        speed = 30;
        rigidbody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.15f);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.15f);
        rigidbody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }
    
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().ReceiveDamage(damage, transform.right);
        }
    }
}
