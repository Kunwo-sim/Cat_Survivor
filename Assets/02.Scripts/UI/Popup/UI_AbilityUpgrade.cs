using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_AbilityUpgrade : UI_Popup
{
    enum GameObjects
    {
        AbilityPanels,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        // Bind �Լ��� ���÷������� Enum �ѱ�
        Bind<GameObject>(typeof(GameObjects)); ;

        GameObject AbilityPanels = Get<GameObject>((int)GameObjects.AbilityPanels);
        foreach (Transform child in AbilityPanels.transform)
        {
            Destroy(child.gameObject);
        }

        List<AbilityData> data = AbilityGetter.GetRandomAbility();
        for (int i = 0; i < 4; i++)
        {
            UI_AbilityPanel AbilityPanel = UIManager.Instance.MakeSubUI<UI_AbilityPanel>("UI_AbilityPanel");

            AbilityPanel.transform.SetParent(AbilityPanels.transform);
            AbilityPanel.name = "AbilityPanel" + i;
            AbilityPanel.SetAbilityPanel(data[i]);
        }
    }
}
