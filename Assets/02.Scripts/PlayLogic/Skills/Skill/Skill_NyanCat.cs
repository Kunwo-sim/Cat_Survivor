using UnityEngine;
public class Skill_NyanCat : Skill
{
    public override void Activate(Transform holderTrans)
    {
        // 스킬 생성 위치 설정 (무기)
        Transform spawnTransform = holderTrans;

        // 오브젝트 생성
        Projectile cloneProjectile = ObjectPoolManager.GetObject(poolType).GetComponent<Projectile>();
        cloneProjectile.Initialize(spawnTransform.position, spawnTransform.rotation, damage, activeTime, poolType);
        SoundManager.Instance.PlaySFXSound("SkillShot");
    }
}