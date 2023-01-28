using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Bong : Skill
{
    protected override IEnumerator Pattern()
    {
        var dir = targeting.GetToNearDirection(holderPos);
        var pos = holderPos + targeting.GetToNearPosition(dir, 1);
        var rot = targeting.GetToNearRotate(dir);
        
        cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_Bong>());
        cloneProjectile[0].GetComponent<Projectile_Bong>().Initialize(pos, rot, damage + Level*5, activeTime, poolType, _holderTrans);
        SoundManager.Instance.PlaySFXSound("SkillShot");
        yield break;
    }
}
