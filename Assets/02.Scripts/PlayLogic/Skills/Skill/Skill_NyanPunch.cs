using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Skill_NyanPunch : Skill
{
    protected override IEnumerator Pattern()
    {
        Vector3 nearDir = targeting.GetToNearDirection(holderPos);
        for (int i = 0; i < 4; i++)
        {
            // float alpha = (i == 0) ? 1f : 0.3f;
            // alpha = 1f;

            int space = 30;
            var dir = Quaternion.AngleAxis(i*space - space*1.5f, Vector3.forward) * nearDir;
            var pos = holderPos + targeting.GetToNearPosition(dir);
            var rot = targeting.GetToNearRotate(dir);

            cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanPunch>());
            cloneProjectile[i].Initialize(pos, rot, damage + Level*5, activeTime, poolType);
            SoundManager.Instance.PlaySFXSound("SkillShot");
            yield return new WaitForSeconds(0.05f);
        }
    }
}