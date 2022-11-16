using System.Collections.Generic;
using UnityEngine;

namespace PlayLogic
{
    using System;
    public class SkillHolder : MonoBehaviour
    {
        public List<SkillInfo> skillList = new List<SkillInfo>();

        private void Awake()
        {
            foreach (SkillInfo skill in skillList)
            {
                skill.NextCoolDown = 0;
            }
        }

        private void Update()
        {
            CheckCoolDown();
        }

        private void CheckCoolDown()
        {
            foreach (SkillInfo skill in skillList)
            {
                bool coolDownComplete = (Time.time > skill.NextCoolDown);
                if (coolDownComplete)
                {
                    ActiveSkill(skill);
                }
            }
        }

        private void ActiveSkill(SkillInfo skillInfo)
        {
            skillInfo.NextCoolDown = skillInfo.BaseCoolDown + Time.time;
            skillInfo.Activate(transform);
        }
    }
}