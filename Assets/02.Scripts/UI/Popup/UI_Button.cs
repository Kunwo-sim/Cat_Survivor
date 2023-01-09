using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObj,
    }

    enum Images
    {
        ItemIcon,
    }

    int _score = 0;
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        // Bind 함수에 리플렉션으로 Enum 넘김
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GameObject scoreBtn = Get<Button>((int)Buttons.PointButton).gameObject;
        BindEvent(scoreBtn, OnBtnClicked, Define.UIEvent.Click);

        GameObject go = Get<Image>((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }
    public void OnBtnClicked(PointerEventData data)
    {
        _score++;
        Get<TextMeshProUGUI>((int)Texts.ScoreText).text = $"Score : {_score}";
    }
}
