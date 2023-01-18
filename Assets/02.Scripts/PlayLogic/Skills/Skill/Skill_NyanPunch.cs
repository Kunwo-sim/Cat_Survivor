using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Skill_NyanPunch : Skill
{

    public override void Activate(Transform holderTrans)
    {
        // 스킬 생성 위치 설정 (무기)
        spawnTransform = holderTrans;

        cloneProjectile.Clear();
        // 오브젝트 생성
        for (int i = 0; i < 4; i++)
            cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile>());

        foreach (var projectile in cloneProjectile)
        {
            projectile.Initialize(spawnTransform.position, spawnTransform.rotation, damage, activeTime, poolType);
            SoundManager.Instance.PlaySFXSound("SkillShot");
        }
    }
}