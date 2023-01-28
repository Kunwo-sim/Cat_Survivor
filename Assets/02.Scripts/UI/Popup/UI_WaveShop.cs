using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_WaveShop : UI_Popup
{
    int _reRollCost = 10;
    Player _player;
    GameObject ItemPanels;
    enum Texts
    {
        WaveInfo,
        CoinText,
        ReRollButtonText,

        MaxHp,
        HpRegen,
        MeleeAttack,
        RangeAttack,
        MoveSpeed,
        Defense,
        Attack,
        Critical,
        AttackSpeed,
        Range
    }

    enum GameObjects
    {
        ItemPanels,
        ReRollButton,
        NextButton
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
        Bind<GameObject>(typeof(GameObjects));;

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

        ItemPanels = Get<GameObject>((int)GameObjects.ItemPanels);

        SetItemPanels();
        _reRollCost = 10;
        Get<TextMeshProUGUI>((int)Texts.ReRollButtonText).text = $"초기화 - {_reRollCost}";

        Get<GameObject>((int)GameObjects.NextButton).BindEvent(OnNextButtonClicked, Define.UIEvent.Click);
        Get<GameObject>((int)GameObjects.ReRollButton).BindEvent(OnReRollButtonClicked, Define.UIEvent.Click);
    }
    void Update()
    {
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

        Get<TextMeshProUGUI>((int)Texts.CoinText).text = InGameManager.Instance.Money.ToString();
    }
    void SetItemPanels()
    {
        foreach (Transform child in ItemPanels.transform)
        {
            Destroy(child.gameObject);
        }

        List<ItemData> data = ItemGetter.GetRandomItem();
        for (int i = 0; i < 4; i++)
        {
            GameObject itemPanel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Sub/UI_ItemPanel"));
            itemPanel.transform.SetParent(ItemPanels.transform);
            itemPanel.name = "ItemPanel" + i;

            UI_ItemPanel item = itemPanel.GetOrAddComponent<UI_ItemPanel>();
            item.SetItemInfo(data[i]);
        }
    }
    void OnNextButtonClicked(PointerEventData data)
    {
        UIManager.Instance.ClosePopupUI();
        WaveManager.Instance.BeforeWaveStart();
    }
    void OnReRollButtonClicked(PointerEventData data)
    {
        if (InGameManager.Instance.Money < _reRollCost)
            return;

        InGameManager.Instance.Money -= _reRollCost;
        SetItemPanels();
        _reRollCost += 2;
        Get<TextMeshProUGUI>((int)Texts.ReRollButtonText).text = $"초기화 - {_reRollCost}";
        Get<TextMeshProUGUI>((int)Texts.CoinText).text = InGameManager.Instance.Money.ToString();
    }
}
