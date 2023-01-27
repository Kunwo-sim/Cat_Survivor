using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Reflection;

public class UI_ItemPanel : UI_Base
{
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

        // functionName과 동일한 함수 호출
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
    }

    public void Item1()
    {
        _player.MeleeAttack += 4;
        _player.Defense -= 2;
    }
    public void Item2()
    {
        _player.Defense += 5;
        _player.MoveSpeed -= 3;
    }
    public void Item3()
    {
        _player.Critical += 5;
    }
    public void Item4()
    {
        _player.MaxHp += 6;
        _player.Attack -= 3;
    }
    public void Item5()
    {
        _player.AttackSpeed += 10;
        _player.RangeAttack -= 1;
    }
    public void Item6()
    {
        _player.MaxHp += 2;
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

    }
    public void Item12()
    {

    }
    public void Item13()
    {

    }
    public void Item14()
    {

    }
}
