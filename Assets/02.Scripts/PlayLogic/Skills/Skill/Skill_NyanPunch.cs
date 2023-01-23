using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Skill_NyanPunch : Skill
{
    private List<Projectile_NyanPunch> cloneProjectile = new List<Projectile_NyanPunch>();
    
    protected override void Initialize(Transform holderTrans)
    {
        base.Initialize(holderTrans);
        cloneProjectile.Clear();
    }
    public override void Activate(Transform holderTrans)
    {
        base.Activate(holderTrans);
    }

    protected override IEnumerator Pattern()
    {
        Vector3 holderPos = spawnTransform.position;
        Vector3 nearDir = targeting.GetToNearDirection(holderPos);

        for (int i = 0; i < 4; i++)
        {
            // Test
            float alpha = (i == 0) ? 1f : 0.3f;
            alpha = 1f;
            
            Vector3 dir = Quaternion.AngleAxis(i*20 - 30, Vector3.forward) * nearDir;
            Quaternion rot = targeting.DirectToRotate(dir);
            Vector3 pos = targeting.GetNearSpawnPosition(holderPos, dir);
            
            cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanPunch>());
            cloneProjectile[i].Initialize(pos, rot, damage, activeTime, poolType, alpha);
            SoundManager.Instance.PlaySFXSound("SkillShot");
            yield return new WaitForSeconds(0.05f);
        }
    }
}