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
        StartCoroutine(Pattern());
    }

    IEnumerator Pattern()
    {
        for (int i = 0; i < 4; i++)
        {
            var pos = spawnTransform.position + spawnTransform.up * 2.5f + spawnTransform.right * Random.Range(-0.7f, 0.7f);
            cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanPunch>());
            cloneProjectile[i].Initialize(pos , spawnTransform.rotation, damage, activeTime, poolType);
            SoundManager.Instance.PlaySFXSound("SkillShot");
            yield return new WaitForSeconds(0.05f);
        }
    }
}