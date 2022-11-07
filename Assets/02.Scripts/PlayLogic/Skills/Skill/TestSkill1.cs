using UnityEngine;

namespace PlayLogic
{
    [CreateAssetMenu(fileName = "TestSkill1", menuName = "Skills/TestSkill1")]
    public class TestSkill1 : Skill
    {
        public override void Activate(Transform holderTrans)
        {
            // 스킬 생성 위치 설정 (무기)
            Transform spawnTransform = holderTrans;
            
            // 오브젝트 생성 (오브젝트풀링 예정)
            Projectile cloneProjectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
            cloneProjectile.Initialize(spawnTransform.position, spawnTransform.rotation, damage, activeTime);
        }
    }
}