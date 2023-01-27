using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int Key;
    public string Name;
    public string Kind;
    public string Description;
    public int Power;
    public string FunctionName;
    public int Cost;
    public string Grade;
    public ItemData(int key, string name, string kind, string description, string functionName, int cost, string grade)
    {
        Key = key;
        Name = name;
        Kind = kind;
        Description = description;
        FunctionName = functionName;
        Cost = cost;
        Grade = grade;
    }
}

public static class ItemGetter
{
    private static List<Dictionary<string, object>> _itemCSV;
    private static Dictionary<int, ItemData> _itemData;
    static ItemGetter()
    {

        _itemCSV = CSVReader.Read("Item");
        _itemData = new Dictionary<int, ItemData>();

        for (int i = 0; i < _itemCSV.Count; i++)
        {
            int key = int.Parse(_itemCSV[i]["key"].ToString());
            string name = _itemCSV[i]["name"].ToString();
            string kind = _itemCSV[i]["kind"].ToString();
            string description = _itemCSV[i]["description"].ToString();
            description.Replace("\\n", "\n");
            string functionName = _itemCSV[i]["functionName"].ToString();
            int cost = int.Parse(_itemCSV[i]["cost"].ToString());
            string grade = _itemCSV[i]["grade"].ToString();

            ItemData data = new ItemData(key, name, kind, description, functionName, cost, grade);
            _itemData.Add(i, data);
        }
    }

    public static List<ItemData> GetRandomItem()
    {
        List<bool> check = new List<bool>();
        int cnt = Count();
        for (int i = 0; i < cnt; i++)
        {
            check.Add(false);
        }

        List<ItemData> item = new List<ItemData>();
        for (int i = 0; i < 4; i ++)
        {
            while(true)
            {
                int idx = Random.Range(0, Count());
                if (check[idx] == true)
                    continue;

                item.Add(Get(idx));
                check[idx] = true;
                break;
            }
        }

        return item;
    }

    public static int Count()
    {
        return _itemData.Count;
    }
    public static ItemData Get(int key)
    {
        return _itemData[key];
    }
}

