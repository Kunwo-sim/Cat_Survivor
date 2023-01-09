using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class LevelUpSkillPanel : MonoBehaviour
{
    private TextMeshProUGUI _skillName;
    private TextMeshProUGUI _skillDescription;

    private List<Dictionary<string, object>> _skillsData;
    private void Awake()
    {
        _skillName = GetComponentsInChildren<TextMeshProUGUI>()[0];
        _skillDescription = GetComponentsInChildren<TextMeshProUGUI>()[1];
        _skillsData = CSVReader.Read ("Skills");
    }
    

    public void SetSkill(int index)
    {
        _skillName.text = SkillGetter.GetSkillData(4).Name;
        _skillDescription.text = SkillGetter.GetSkillData(4).Description;
    }
}