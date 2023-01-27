using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_WaveShop : UI_Popup
{
    Player _player;
    enum Texts
    {
        WaveInfo,
        CoinCount,
        RerollText,

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

        // 플레이어의 능력치를 가져와 셋팅
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

        GameObject ItemPanels = Get<GameObject>((int)GameObjects.ItemPanels);
        foreach (Transform child in ItemPanels.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < 4; i++)
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
        Time.timeScale = 1.0f;
        WaveManager.Instance.BeforeWaveStart();
    }
}
