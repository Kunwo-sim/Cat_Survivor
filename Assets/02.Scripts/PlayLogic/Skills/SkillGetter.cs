using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CSV를 변환 받을 클래스
public class SkillData
{
    public int Key;
    public string Name;
    public string Description;
    public int Power;
    public float Cooldown;

    public SkillData(int key, string name, string description, int power, float cooldown)
    {
        Key = key;
        Name = name;
        Description = description;
        Power = power;
        Cooldown = cooldown;
    }
}

public static class SkillGetter
{
    private static List<Dictionary<string, object>> _skillCSV;
    private static Dictionary<int, SkillData> _skillData;
    static SkillGetter()
    {
        _skillCSV = CSVReader.Read("Skills");
        _skillData = new Dictionary<int, SkillData>();
        for (int i = 0; i < _skillCSV.Count; i++)
        {
            int key = int.Parse(_skillCSV[i]["key"].ToString());
            string name = _skillCSV[i]["name"].ToString();
            string description = _skillCSV[i]["description"].ToString();
            int power = int.Parse(_skillCSV[i]["power"].ToString());
            float coolDown = float.Parse(_skillCSV[i]["cooldown"].ToString());

            SkillData skillData = new SkillData(key, name, description, power, coolDown);
            _skillData.Add(i, skillData);
        }
    }

    public static int Count()
    {
        return _skillData.Count;
    }
    public static SkillData GetSkillData(int key)
    {
        return _skillData[key];
    }
}