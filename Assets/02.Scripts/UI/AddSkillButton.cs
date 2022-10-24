using PlayLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AddSkillButton : MonoBehaviour
    {
        private SkillHolder _skillHolder;
        private void Awake()
        {
            _skillHolder = GameObject.FindWithTag("Player").GetComponentInChildren<SkillHolder>();
        }

        public void AddSkill(Skill skill)
        {
            skill.NextCoolDown = 0;
            _skillHolder.Skills.Add(skill);
        }
        
        
    }
}
