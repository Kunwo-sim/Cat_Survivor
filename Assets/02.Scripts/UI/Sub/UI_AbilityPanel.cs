using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UI_AbilityPanel : UI_Base
{
    enum GameObjects
    {
        NamePanel,
        IconPanel,
        DescriptionPanel
    }
    enum Texts
    {
        NameText,
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
        gameObject.BindEvent(OnAbilityPanelClicked, Define.UIEvent.Click);
    }

    public void OnAbilityPanelClicked(PointerEventData data)
    {
        Debug.Log("어빌리티 선택");
        UIManager.Instance.ClosePopupUI();
        UIManager.Instance.ShowPopupUI<UI_WaveShop>("UI_WaveShop");
    }

    public void SetAbilityPanel(AbilityData data)
    {
        Get<TextMeshProUGUI>((int)Texts.NameText).text = data.Name;
        Get<TextMeshProUGUI>((int)Texts.DescriptionText).text = data.Description;
    }
}
