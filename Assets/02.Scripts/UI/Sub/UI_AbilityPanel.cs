using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using System;
using System.Reflection;
using UnityEngine.UI;

public class UI_AbilityPanel : UI_Base
{
    int _abilityValue = 0;
    Player _player;
    string _functionName;
    enum GameObjects
    {
        SelectButton,
        AbilityBackGround,
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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.SelectButton).BindEvent(OnAbilityPanelClicked, Define.UIEvent.Click);
    }

    public void OnAbilityPanelClicked(PointerEventData data)
    {
        // functionName�� ������ �Լ� ȣ��
        Type thisType = GetType();
        MethodInfo theMethod = thisType.GetMethod(_functionName);
        theMethod.Invoke(this, null);

        if (_player.LevelCnt > 0)
        {
            _player.LevelCnt -= 1;
            UIManager.Instance.ClosePopupUI();
            UIManager.Instance.ShowPopupUI<UI_AbilityUpgrade>("UI_AbilityUpgrade");
        }
        else
        {
            UIManager.Instance.ClosePopupUI();
            UIManager.Instance.ShowPopupUI<UI_WaveShop>("UI_WaveShop");
        }
    }

    public void SetAbilityPanel(AbilityData data)
    {
        int grade = UnityEngine.Random.Range(0, 101);
        _abilityValue = data.Power;
        _functionName = data.FunctionName;

        if (grade <= InGameManager.Instance._normalAbilityPercnt)
        {

        }
        else if (grade <= InGameManager.Instance._rareAbilityPercnt)
        {
            _abilityValue *= 2;
            Get<GameObject>((int)GameObjects.AbilityBackGround).GetComponent<Image>().sprite = InGameManager.Instance.abilityPanelSprite[1];
        }
        else if (grade <= InGameManager.Instance._epicAbilityPercnt)
        {
            _abilityValue *= 3;
            Get<GameObject>((int)GameObjects.AbilityBackGround).GetComponent<Image>().sprite = InGameManager.Instance.abilityPanelSprite[2];
        }
        else
        {
            _abilityValue *= 4;
            Get<GameObject>((int)GameObjects.AbilityBackGround).GetComponent<Image>().sprite = InGameManager.Instance.abilityPanelSprite[2];
        }
        Get<TextMeshProUGUI>((int)Texts.DescriptionText).text = $"{data.Description}+{_abilityValue}{data.Type}";
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }

    public void AbilityMaxHp()
    {
        _player.MaxHp += _abilityValue;
    }
    public void AbilityHpRegen()
    {
        _player.HpRegen += _abilityValue;
    }
    public void AbilityMeleeAttack()
    {
        _player.MeleeAttack += _abilityValue;
    }
    public void AbilityRangeAttack()
    {
        _player.RangeAttack += _abilityValue;
    }
    public void AbilityMoveSpeed()
    {
        _player.MoveSpeed += _abilityValue;
    }
    public void AbilityDefense()
    {
        _player.Defense += _abilityValue;
    }
    public void AbilityAttack()
    {
        _player.Attack += _abilityValue;
    }
    public void AbilityCritical()
    {
        _player.Critical += _abilityValue;
    }
    public void AbilityAttackSpeed()
    {
        _player.AttackSpeed += _abilityValue;
    }
    public void AbilityRange()
    {
        _player.AttackRange += _abilityValue;
    }
}
