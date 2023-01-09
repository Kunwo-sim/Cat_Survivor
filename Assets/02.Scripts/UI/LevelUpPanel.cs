using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUpPanel : MonoBehaviour
{
    private readonly List<LevelUpSkillPanel> _skillPanels = new List<LevelUpSkillPanel>();
    private void Awake()
    {
        _skillPanels.AddRange(GetComponentsInChildren<LevelUpSkillPanel>());
    }

    public void SetSkillPanels()
    {
        foreach (var panel in _skillPanels)
        {
            panel.SetSkill(Random.Range(0, 3));
        }
    }
}