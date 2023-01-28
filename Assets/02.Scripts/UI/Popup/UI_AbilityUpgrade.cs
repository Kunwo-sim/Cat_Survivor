using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_AbilityUpgrade : UI_Popup
{
    int _reRollCost = 5;
    Player _player;
    GameObject AbilityPanels;
    enum Texts
    {
        WaveInfoText,
        MaxHp,
        HpRegen,
        MeleeAttack,
        RangeAttack,
        MoveSpeed,
        Defense,
        Attack,
        Critical,
        AttackSpeed,
        Range,

        ReRollButtonText,
    }
    enum GameObjects
    {
        AbilityPanels,
        ReRollButton,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        // Bind �Լ��� ���÷������� Enum �ѱ�
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        // �÷��̾��� �ɷ�ġ�� ������ ����
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Get<TextMeshProUGUI>((int)Texts.MaxHp).text = ((int)_player.MaxHp).ToString();
        Get<TextMeshProUGUI>((int)Texts.HpRegen).text = _player.HpRegen.ToString();
        Get<TextMeshProUGUI>((int)Texts.MeleeAttack).text = _player.MeleeAttack.ToString();
        Get<TextMeshProUGUI>((int)Texts.RangeAttack).text = _player.RangeAttack.ToString();
        Get<TextMeshProUGUI>((int)Texts.MoveSpeed).text = _player.MoveSpeed.ToString();
        Get<TextMeshProUGUI>((int)Texts.Defense).text = _player.Defense.ToString();
        Get<TextMeshProUGUI>((int)Texts.Attack).text = _player.Attack.ToString();
        Get<TextMeshProUGUI>((int)Texts.Critical).text = _player.Critical.ToString();
        Get<TextMeshProUGUI>((int)Texts.AttackSpeed).text = _player.AttackSpeed.ToString();
        Get<TextMeshProUGUI>((int)Texts.Range).text = _player.AttackRange.ToString();

        Get<GameObject>((int)GameObjects.ReRollButton).BindEvent(OnReRollButtonClicked, Define.UIEvent.Click);

        AbilityPanels = Get<GameObject>((int)GameObjects.AbilityPanels);
        SetAbilityPanels();
        _reRollCost = 5;
        Get<TextMeshProUGUI>((int)Texts.ReRollButtonText).text = $"초기화 : {_reRollCost}";
    }
    void SetAbilityPanels()
    {
        foreach (Transform child in AbilityPanels.transform)
        {
            Destroy(child.gameObject);
        }
        // 4���� ������ �����Ƽ�� �������� �ʱ�ȭ
        List<AbilityData> data = AbilityGetter.GetRandomAbility();
        for (int i = 0; i < 4; i++)
        {
            UI_AbilityPanel AbilityPanel = UIManager.Instance.MakeSubUI<UI_AbilityPanel>("UI_AbilityPanel");

            AbilityPanel.transform.SetParent(AbilityPanels.transform);
            AbilityPanel.name = "AbilityPanel" + i;
            AbilityPanel.SetAbilityPanel(data[i]);
        }
    }

    void OnReRollButtonClicked(PointerEventData data)
    {
        if (InGameManager.Instance.Money < _reRollCost)
            return;

        InGameManager.Instance.Money -= _reRollCost;
        SetAbilityPanels();
        _reRollCost += 2;
        Get<TextMeshProUGUI>((int)Texts.ReRollButtonText).text = $"초기화 : {_reRollCost}";
    }
}
