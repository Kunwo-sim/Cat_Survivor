using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill_Orb : Skill
{
    protected override IEnumerator Pattern()
    {
        var dir = targeting.GetToNearDirection(holderPos);
        var pos = holderPos + targeting.GetToNearPosition(dir, 1);
        var rot = targeting.GetToNearRotate(dir);
        
        cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_Orb>());
        cloneProjectile[0].Initialize(pos, rot, damage, activeTime, poolType);
        SoundManager.Instance.PlaySFXSound("Orb_Shot");
        yield break;
    }
}