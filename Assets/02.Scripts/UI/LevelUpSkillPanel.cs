using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
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
        int key = Random.Range(0, SkillGetter.Count());
        _skillName.text = SkillGetter.Get(key).Name;
        _skillDescription.text = SkillGetter.Get(key).Description;
    }
}