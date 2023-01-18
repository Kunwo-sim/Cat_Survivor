using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Skill_NyanPunch : Skill
{
    private new List<Projectile_NyanPunch> cloneProjectile = new List<Projectile_NyanPunch>();
    
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
            float alpha = (i == 0) ? 1f : 0.3f;
            var pos = spawnTransform.position + spawnTransform.up * 2.5f + spawnTransform.right * Random.Range(-0.8f, 0.8f);
            cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanPunch>());
            cloneProjectile[i].Initialize(pos, spawnTransform.rotation, damage, activeTime, poolType, alpha);
            SoundManager.Instance.PlaySFXSound("SkillShot");
            yield return new WaitForSeconds(0.05f);
        }
    }
}