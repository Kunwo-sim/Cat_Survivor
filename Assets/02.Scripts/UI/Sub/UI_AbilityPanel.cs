using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using System;
using System.Reflection;

public class UI_AbilityPanel : UI_Base
{
    Player _player;
    string _functionName;
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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.SelectButton).BindEvent(OnAbilityPanelClicked, Define.UIEvent.Click);
    }

    public void OnAbilityPanelClicked(PointerEventData data)
    {
        // functionName과 동일한 함수 호출
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
        Get<TextMeshProUGUI>((int)Texts.DescriptionText).text = data.Description;
        _functionName = data.FunctionName;
    }

    public void AbilityMaxHp()
    {
        _player.MaxHp += 3;
    }
    public void AbilityHpRegen()
    {
        _player.HpRegen += 1;
    }
    public void AbilityMeleeAttack()
    {
        _player.MeleeAttack += 1;
    }
    public void AbilityRangeAttack()
    {
        _player.RangeAttack += 1;
    }
    public void AbilityMoveSpeed()
    {
        _player.MoveSpeed += 3;
    }
    public void AbilityDefense()
    {
        _player.Defense += 2;
    }
    public void AbilityAttack()
    {
        _player.Attack += 4;
    }
    public void AbilityCritical()
    {
        _player.Critical += 4;
    }
    public void AbilityAttackSpeed()
    {
        _player.AttackSpeed += 3;
    }
    public void AbilityRange()
    {
        _player.AttackRange += 10;
    }
}
