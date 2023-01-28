using System.Collections.Generic;
using UnityEngine;
public class SkillHolder : MonoBehaviour
{
    
    public List<Skill> skillList = new List<Skill>();
    public List<Skill> skillData = new List<Skill>();
    private void Awake()
    {
        foreach (Skill skill in skillList)
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
        foreach (Skill skill in skillList)
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