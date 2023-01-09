using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText
    }

    string _name;

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TextMeshProUGUI>().text = _name;
        BindEvent(Get<GameObject>((int)GameObjects.ItemIcon), OnIconClick);
    }

    public void OnIconClick(PointerEventData data)
    {
        Debug.Log($"{_name}이 클릭되었음");
    }
    public void SetInfo(string name)
    {
        _name = name;
    }
}
