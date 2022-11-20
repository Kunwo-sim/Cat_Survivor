using System.Collections;
using UnityEngine;

public class TestProjectile1 : Projectile
{
    protected override IEnumerator Move()
    {
        Speed = 20;
        Vector2 force = transform.up.normalized * Speed;
        Rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        yield return null;
    }
}