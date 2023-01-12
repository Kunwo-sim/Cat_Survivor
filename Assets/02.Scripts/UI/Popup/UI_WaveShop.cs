using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_WaveShop : UI_Popup
{
    enum Texts
    {
        ShopName,
        CoinCount
    }

    enum GameObjects
    {
        ItemPanels,
        NextButton
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        // Bind 함수에 리플렉션으로 Enum 넘김
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));;

        GameObject ItemPanels = Get<GameObject>((int)GameObjects.ItemPanels);
        foreach (Transform child in ItemPanels.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < 8; i++)
        {
            GameObject itemPanel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Sub/UI_ItemPanel"));
            itemPanel.transform.SetParent(ItemPanels.transform);
            itemPanel.name = "ItemPanel" + i;

            UI_ItemPanel item = itemPanel.GetOrAddComponent<UI_ItemPanel>();
        }

        Get<GameObject>((int)GameObjects.NextButton).BindEvent(OnNextButtonClicked, Define.UIEvent.Click);
    }

    void OnNextButtonClicked(PointerEventData data)
    {
        UIManager.Instance.ClosePopupUI();
    }
}
