using UnityEngine;
public class Skill_NyanCat : Skill
{
    public override void Activate(Transform holderTrans)
    {
        // 스킬 생성 위치 설정 (무기)
        spawnTransform = holderTrans;

        cloneProjectile.Clear();
        // 오브젝트 생성
        cloneProjectile.Add(ObjectPoolManager.GetObject(poolType).GetComponent<Projectile>());
        cloneProjectile[0].Initialize(spawnTransform.position, spawnTransform.rotation, damage, activeTime, poolType);
        SoundManager.Instance.PlaySFXSound("SkillShot");
    }
}