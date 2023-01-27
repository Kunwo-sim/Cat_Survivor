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
    }
    public void SetItemInfo(ItemData data)
    {
        Get<TextMeshProUGUI>((int)Texts.ItemName).text = data.Name;
        Get<TextMeshProUGUI>((int)Texts.ItemKind).text = data.Kind;
        Get<TextMeshProUGUI>((int)Texts.ItemDescription).text = data.Description;
        Get<TextMeshProUGUI>((int)Texts.CostText).text = $"비용 : {data.Cost}";
    }
}
