using System.Collections;
using UnityEngine;

public class Projectile_NyanCat : Projectile
{
    protected override void Awake()
    {
        base.Awake();
        _projectileType = Define.ProjectileType.Range;
    }
    protected override IEnumerator Move()
    {
        speed = 20;
        Vector2 force = transform.right.normalized * speed;
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);

        // Test
        spriteRenderer.flipY = transform.eulerAngles.z is > 90 and <= 270;

        yield break;
    }
}