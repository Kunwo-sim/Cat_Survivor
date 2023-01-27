using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_NyanPunch : Projectile
{
    public void Initialize(Vector2 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType, float alpha)
    {
        base.Initialize(spawnPos, spawnRot, damage, activeTime, poolType);
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
        _projectileType = Define.ProjectileType.Melee;
    }
    protected override IEnumerator Move()
    {
        yield break;
    }
}