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
        Vector3 pos = spawnTransform.position;
        GameObject targetObject = Targeting.GetNearGameObject(pos);
        if (targetObject == null) yield break;
        Vector3 targetPos = targetObject.transform.position;
        Vector3 firstDir = Targeting.GetDirection(pos, targetPos);

        for (int i = 0; i < 4; i++)
        {
            float alpha = (i == 0) ? 1f : 0.3f;
            Vector3 dir = Quaternion.AngleAxis(i*10 - 15, Vector3.forward) * firstDir; 
            Quaternion rot = Targeting.GetLookAtQuaternion(dir);
            Vector3 spawnPos = (Vector3)pos + dir * 3;
            
            cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile_NyanPunch>());
            cloneProjectile[i].Initialize(spawnPos, rot, damage, activeTime, poolType, alpha);
            SoundManager.Instance.PlaySFXSound("SkillShot");
            
            yield return new WaitForSeconds(0.05f);
        }
    }
}