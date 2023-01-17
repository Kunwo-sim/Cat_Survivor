using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestButtons : MonoBehaviour
{
    private SkillCoolManager _skillCoolManager;
    private void Awake()
    {
        _skillCoolManager = GameObject.FindWithTag("Player").GetComponentInChildren<SkillCoolManager>();
    }

    public void AddSkill(Skill skill)
    {
        skill.NextCoolDown = 0;
        _skillCoolManager.skillList.Add(skill);
    }

    public void TempAddSkillButton()
    {
        Time.timeScale = 1;
        GameObject.Find("LevelUpPanel").SetActive(false);
    }

    public void TempGameOverButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
    }
}