using UnityEngine;

[CreateAssetMenu(fileName = "Skill1", menuName = "Skills/Skill1")]
public class SkillInfo : ScriptableObject
{
    [SerializeField] private new string name = "New Skill";
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float activeTime = 3f;

    [SerializeField] protected EPoolObjectType poolType;
    [SerializeField] protected GameObject projectilePrefab;


    [field: SerializeField] public float BaseCoolDown { get; set; } = 1;
    public float NextCoolDown { get; set; } = 0;

    public void Activate(Transform holderTrans)
    {
        // 스킬 생성 위치 설정 (무기)
        Transform spawnTransform = holderTrans;

        // 오브젝트 생성
        Projectile cloneProjectile = ObjectPoolManager.GetObject(poolType).GetComponent<Projectile>();
        cloneProjectile.Initialize(spawnTransform.position, spawnTransform.rotation, damage, activeTime, poolType);
        SoundManager.Instance.PlaySFXSound("SkillShot");
    }

    // public override void Activate(Transform holderTrans)
    // {
    //     // 스킬 생성 위치 설정 (무기)
    //     Vector2 spawnPos = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-6.0f, 6.0f));
    //     
    //     // 스킬 방향 및 힘 설정
    //     float rot = Mathf.Atan2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Mathf.Rad2Deg;
    //     Quaternion spawnRot = Quaternion.Euler(0, 0, rot - 90);
    //     
    //     // 오브젝트 생성
    //     Projectile cloneProjectile = ObjectPoolManager.GetObject(poolType).GetComponent<Projectile>();
    //     cloneProjectile.Initialize(spawnPos, spawnRot, damage, activeTime, poolType);
    // }
}