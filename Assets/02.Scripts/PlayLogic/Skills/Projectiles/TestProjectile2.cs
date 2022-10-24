using System.Collections;
using UnityEngine;

namespace PlayLogic
{
    public class TestProjectile2 : Projectile
    {
        protected override IEnumerator Move()
        {
            Speed = 20;
            Vector2 force = transform.up.normalized * Speed;
            Rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            yield return null;
        }
    }
}