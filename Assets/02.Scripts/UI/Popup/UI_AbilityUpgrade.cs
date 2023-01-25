using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_AbilityUpgrade : UI_Popup
{
    Player _player;
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
    }
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
        // Bind 함수에 리플렉션으로 Enum 넘김
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        // 플레이어의 능력치를 가져와 셋팅
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Get<TextMeshProUGUI>((int)Texts.MaxHp).text = ((int)_player.Hp).ToString();
        Get<TextMeshProUGUI>((int)Texts.HpRegen).text = _player.HpRegen.ToString();
        Get<TextMeshProUGUI>((int)Texts.MeleeAttack).text = _player.MeleeAttack.ToString();
        Get<TextMeshProUGUI>((int)Texts.RangeAttack).text = _player.RangeAttack.ToString();
        Get<TextMeshProUGUI>((int)Texts.MoveSpeed).text = _player.MoveSpeed.ToString();
        Get<TextMeshProUGUI>((int)Texts.Defense).text = _player.Defense.ToString();
        Get<TextMeshProUGUI>((int)Texts.Attack).text = _player.Attack.ToString();
        Get<TextMeshProUGUI>((int)Texts.Critical).text = _player.Critical.ToString();
        Get<TextMeshProUGUI>((int)Texts.AttackSpeed).text = _player.AttackSpeed.ToString();
        Get<TextMeshProUGUI>((int)Texts.Range).text = _player.AttackRange.ToString();

        GameObject AbilityPanels = Get<GameObject>((int)GameObjects.AbilityPanels);
        foreach (Transform child in AbilityPanels.transform)
        {
            Destroy(child.gameObject);
        }

        // 4개의 무작위 어빌리티를 가져오고 초기화
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
