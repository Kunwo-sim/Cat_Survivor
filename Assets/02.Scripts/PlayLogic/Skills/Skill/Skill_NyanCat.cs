using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill_NyanCat : Skill
{
    private List<Projectile_NyanCat> cloneProjectile = new List<Projectile_NyanCat>();
    protected override void Initialize(Transform holderTrans)
    {
        base.Initialize(holderTrans);
    }


    protected override IEnumerator Pattern()
    {
        cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanCat>());
        cloneProjectile[0].Initialize(spawnTransform.position, spawnTransform.rotation, damage, activeTime, poolType);
        SoundManager.Instance.PlaySFXSound("SkillShot");
        yield break;
    }
}