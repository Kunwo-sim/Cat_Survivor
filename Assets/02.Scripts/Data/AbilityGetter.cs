using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData
{
    public int Key;
    public string Name;
    public string Description;
    public int Power;
    public string FunctionName;
    public string Type;

    public AbilityData(int key, string name, string description, int power, string functionName, string type)
    {
        Key = key;
        Name = name;
        Description = description;
        Power = power;
        FunctionName = functionName;
        Type = type;
    }
}

public static class AbilityGetter
{
    private static List<Dictionary<string, object>> _abilityCSV;
    private static Dictionary<int, AbilityData> _abilityData;
    static AbilityGetter()
    {

        _abilityCSV = CSVReader.Read("Ability");
        _abilityData = new Dictionary<int, AbilityData>();

        for (int i = 0; i < _abilityCSV.Count; i++)
        {
            int key = int.Parse(_abilityCSV[i]["key"].ToString());
            string name = _abilityCSV[i]["name"].ToString();
            string description = _abilityCSV[i]["description"].ToString();
            int power = int.Parse(_abilityCSV[i]["power"].ToString());
            string functionName = _abilityCSV[i]["functionName"].ToString();
            string type = _abilityCSV[i]["type"].ToString();
            if(string.IsNullOrEmpty(type))
            {
                type = "";
            }

            AbilityData data = new AbilityData(key, name, description, power, functionName, type);
            _abilityData.Add(i, data);
        }
    }

    public static List<AbilityData> GetRandomAbility()
    {
        List<bool> check = new List<bool>();
        int cnt = Count();
        for (int i = 0; i < cnt; i++)
        {
            check.Add(false);
        }

        List<AbilityData> ability = new List<AbilityData>();
        for (int i = 0; i < 4; i ++)
        {
            while(true)
            {
                int idx = Random.Range(0, Count());
                if (check[idx] == true)
                    continue;

                ability.Add(Get(idx));
                check[idx] = true;
                break;
            }
        }

        return ability;
    }

    public static int Count()
    {
        return _abilityData.Count;
    }
    public static AbilityData Get(int key)
    {
        return _abilityData[key];
    }
}

