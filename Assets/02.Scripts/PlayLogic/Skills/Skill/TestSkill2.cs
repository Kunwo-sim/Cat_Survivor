using UnityEngine;

namespace PlayLogic
{
    [CreateAssetMenu(fileName = "TestSkill2", menuName = "Skills/TestSkill2")]
    public class TestSkill2 : Skill
    {
        public override void Activate(SkillHolder holder)
        {
            // 스킬 생성 위치 설정 (무기)
            Vector2 spawnPos = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            
            // 스킬 방향 및 힘 설정
            Vector2 forceVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * force;
            float rot = Mathf.Atan2(forceVector.y, forceVector.x) * Mathf.Rad2Deg;
            Quaternion spawnRot = Quaternion.Euler(0, 0, rot - 90);
            
            // 오브젝트 생성 (오브젝트풀링 예정)
            Projectile cloneProjectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
            cloneProjectile.Initialize(damage, spawnPos, spawnRot, activeTime, forceVector);
        }
    }
}