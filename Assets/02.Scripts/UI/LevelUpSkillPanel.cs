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
        _skillName.text = _skillsData[index]["name"].ToString();
        _skillDescription.text = _skillsData[index]["description"].ToString();
    }
}