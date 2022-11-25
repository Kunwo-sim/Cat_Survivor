using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestButtons : MonoBehaviour
{
    private SkillHolder _skillHolder;
    private void Awake()
    {
        _skillHolder = GameObject.FindWithTag("Player").GetComponentInChildren<SkillHolder>();
    }

    public void AddSkill(SkillInfo skillInfo)
    {
        skillInfo.NextCoolDown = 0;
        _skillHolder.skillList.Add(skillInfo);
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