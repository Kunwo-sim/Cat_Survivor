using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UI_AbilityPanel : UI_Base
{
    enum GameObjects
    {
        SelectButton
    }
    enum Texts
    {
        DescriptionText,
    }

    void Awake()
    {
        Init();
    }
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.SelectButton).BindEvent(OnAbilityPanelClicked, Define.UIEvent.Click);
    }

    public void OnAbilityPanelClicked(PointerEventData data)
    {
        Debug.Log("어빌리티 선택");
        UIManager.Instance.ClosePopupUI();
        UIManager.Instance.ShowPopupUI<UI_WaveShop>("UI_WaveShop");
    }

    public void SetAbilityPanel(AbilityData data)
    {
        Get<TextMeshProUGUI>((int)Texts.DescriptionText).text = data.Description;
    }
}
