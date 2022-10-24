using UnityEngine;

namespace PlayLogic
{
    public abstract class Skill : ScriptableObject
    {
        [SerializeField] private new string name = "New Skill";
        [SerializeField] protected int damage = 1;
        [SerializeField] protected float activeTime = 3f;
        [SerializeField] protected float force = 10f;

        [SerializeField] protected GameObject projectilePrefab;

        public float BaseCoolDown { get; set; } = 1;
        public float NextCoolDown { get; set; } = 0;

        public abstract void Activate(SkillHolder holder);
    }
}