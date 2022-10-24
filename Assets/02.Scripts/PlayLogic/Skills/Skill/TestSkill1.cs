using UnityEngine;

namespace PlayLogic
{
    [CreateAssetMenu(fileName = "TestSkill1", menuName = "Skills/TestSkill1")]
    public class TestSkill1 : Skill
    {
        public override void Activate(SkillHolder holder)
        {
            // 스킬 생성 위치 설정 (무기)
            Transform spawnTransform = holder.transform;
            
            // 스킬 방향 및 힘 설정
            Vector2 forceVector = spawnTransform.up.normalized * force;
            
            // 오브젝트 생성 (오브젝트풀링 예정)
            Projectile cloneProjectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
            cloneProjectile.Initialize(damage, spawnTransform.position, spawnTransform.rotation, activeTime, forceVector);
        }
    }
}