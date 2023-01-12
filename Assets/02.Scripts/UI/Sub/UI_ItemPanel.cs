using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UI_ItemPanel : UI_Base
{
    enum GameObjects
    {
        BackGround,
        ItemImage,
        ItemName,
        ItemKind,
        ItemEffect,
        BuyButton
    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        BindEvent(Get<GameObject>((int)GameObjects.BuyButton), OnBuyButtonClick);
    }

    public void OnBuyButtonClick(PointerEventData data)
    {
        Debug.Log("아이템 구매");
    }
    public void SetInfo(string name)
    {

    }
}
