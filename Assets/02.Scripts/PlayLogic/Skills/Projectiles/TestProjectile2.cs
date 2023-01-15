using System.Collections;
using UnityEngine;

public class TestProjectile2 : Projectile
{
    protected override IEnumerator Move()
    {
        Speed = 20;
        
        
        yield return null;
    }
}