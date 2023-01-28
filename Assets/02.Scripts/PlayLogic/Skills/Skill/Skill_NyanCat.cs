using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill_NyanCat : Skill
{
    protected override IEnumerator Pattern()
    {
        var dir = targeting.GetToNearDirection(holderPos);
        var pos = holderPos + targeting.GetToNearPosition(dir, 1);
        var rot = targeting.GetToNearRotate(dir);
        
        cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanCat>());
        cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanCat>());
        cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanCat>());
        cloneProjectile[0].Initialize(pos, rot * Quaternion.Euler(0,0,-30), damage + Level*5, activeTime, poolType);
        cloneProjectile[1].Initialize(pos, rot, damage + Level*5, activeTime, poolType);
        cloneProjectile[2].Initialize(pos, rot * Quaternion.Euler(0,0,30), damage + Level*5, activeTime, poolType);
        SoundManager.Instance.PlaySFXSound("Gun_Shot");
        yield break;
    }
}