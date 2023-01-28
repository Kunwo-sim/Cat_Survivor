using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Reflection;
using UnityEngine.UI;

public class UI_ItemPanel : UI_Base
{
    string _grade;
    string _functionName;
    int _cost = 0;
    Player _player;
    enum Texts
    {
        ItemName,
        ItemKind,
        ItemDescription,
        CostText,
    }
    enum GameObjects
    {
        BackGround,
        ItemImage,
        BuyButton
    }

    void Awake()
    {
        Init();
    }
    public override void Init()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        BindEvent(Get<GameObject>((int)GameObjects.BuyButton), OnBuyButtonClick);
    }

    public void OnBuyButtonClick(PointerEventData data)
    {
        if (_cost > InGameManager.Instance.Money)
            return;

        // functionName�� ������ �Լ� ȣ��
        Type thisType = GetType();
        MethodInfo theMethod = thisType.GetMethod(_functionName);
        theMethod.Invoke(this, null);

        InGameManager.Instance.Money -= _cost;
        Destroy(gameObject);
    }
    public void SetItemInfo(ItemData data)
    {
        Get<TextMeshProUGUI>((int)Texts.ItemName).text = data.Name;
        Get<TextMeshProUGUI>((int)Texts.ItemKind).text = data.Kind;
        Get<TextMeshProUGUI>((int)Texts.ItemDescription).text = data.Description;
        Get<TextMeshProUGUI>((int)Texts.CostText).text = $"비용 : {data.Cost}";
        _functionName = data.FunctionName;
        _cost = data.Cost;
        _grade = data.Grade;

        Get<GameObject>((int)GameObjects.ItemImage).GetComponent<Image>().sprite = InGameManager.Instance.itemIconSprite[data.Key];

        if (_grade == "normal")
        {
            Get<GameObject>((int)GameObjects.BackGround).GetComponent<Image>().sprite = InGameManager.Instance.itemPanelSprite[0];
        }
        else if (_grade == "rare")
        {
            Get<GameObject>((int)GameObjects.BackGround).GetComponent<Image>().sprite = InGameManager.Instance.itemPanelSprite[1];
        }
        else if (_grade == "epic")
        {
            Get<GameObject>((int)GameObjects.BackGround).GetComponent<Image>().sprite = InGameManager.Instance.itemPanelSprite[2];
        }
        else
        {
            Get<GameObject>((int)GameObjects.BackGround).GetComponent<Image>().sprite = InGameManager.Instance.itemPanelSprite[3];
        }
    }

    public void Item1()
    {
        _player.MeleeAttack += 2;
        _player.RangeAttack += 1;
        _player.Defense -= 2;
    }
    public void Item2()
    {
        _player.Defense += 3;
        _player.HpRegen += 1;
        _player.MoveSpeed -= 3;
    }
    public void Item3()
    {
        _player.Critical += 20;
    }
    public void Item4()
    {
        _player.MaxHp += 6;
        _player.Attack -= 2;
    }
    public void Item5()
    {
        _player.AttackSpeed += 10;
        _player.RangeAttack -= 1;
    }
    public void Item6()
    {
        _player.MaxHp += 3;
        _player.MoveSpeed += 2;
        _player.HpRegen -= 1;
    }
    public void Item7()
    {
        _player.RangeAttack += 2;
        _player.HpRegen -= 1;
    }
    public void Item8()
    {
        _player.MoveSpeed += 5;
        _player.MaxHp -= 2;
    }
    public void Item9()
    {
        _player.Attack += 3;
    }
    public void Item10()
    {
        _player.HpRegen += 2;
        _player.MeleeAttack -= 1;
    }
    public void Item11()
    {
        SkillHolder holder = _player.GetComponentInChildren<SkillHolder>(); 
        if(holder.skillData[0].Level == 0) holder.skillList.Add(holder.skillData[0]);
        holder.skillData[0].Level += 1;
    }
    public void Item12()
    {
        SkillHolder holder = _player.GetComponentInChildren<SkillHolder>(); 
        if(holder.skillData[1].Level == 0) holder.skillList.Add(holder.skillData[1]);
        holder.skillData[1].Level += 1;
    }
    public void Item13()
    {
        SkillHolder holder = _player.GetComponentInChildren<SkillHolder>(); 
        if(holder.skillData[2].Level == 0) holder.skillList.Add(holder.skillData[2]);
        holder.skillData[2].Level += 1;
    }
    public void Item14()
    {
        SkillHolder holder = _player.GetComponentInChildren<SkillHolder>(); 
        if(holder.skillData[3].Level == 0) holder.skillList.Add(holder.skillData[3]);
        holder.skillData[3].Level += 1;
    }
    public void Item15()
    {
        _player.Critical += 5;
        _player.MeleeAttack += 2;
        _player.RangeAttack -= 1;
    }
    public void Item16()
    {
        _player.MaxHp += 2;
        _player.HpRegen += 1;
        _player.Defense -= 1;
    }
}
