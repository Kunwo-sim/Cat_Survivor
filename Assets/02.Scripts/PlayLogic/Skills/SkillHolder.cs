using System.Collections.Generic;
using UnityEngine;

namespace PlayLogic
{
    public class SkillHolder : MonoBehaviour
    {
        public List<Skill> Skills = new List<Skill>();

        private void Update()
        {
            CheckCoolDown();
        }

        private void CheckCoolDown()
        {
            foreach (var skill in Skills)
            {
                bool coolDownComplete = (Time.time > skill.NextCoolDown);
                if (coolDownComplete)
                {
                    ActiveSkill(skill);
                }
            }
        }

        private void ActiveSkill(Skill skill)
        {
            skill.NextCoolDown = skill.BaseCoolDown + Time.time;
            skill.Activate(transform);
        }
    }
}