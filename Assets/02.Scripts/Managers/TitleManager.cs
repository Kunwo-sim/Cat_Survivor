using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI Text_Title;
    public Button Button_Start;
    public void Button_Start_Clicked()
    {
        SceneManager.LoadScene("Lobby");
    }

    void Start()
    {
        Text_Title = GameObject.Find("Text_Title").GetComponent<TextMeshProUGUI>();
        Button_Start = GameObject.Find("Button_Start").GetComponent<Button>();
        TitleFadeEffect();
        ButtonPadeEffect();
    }

    public void TitleFadeEffect()
    {
        Text_Title.DOFade(0.0f, 1.0f).SetLoops(-1, LoopType.Yoyo);
    }

    public void ButtonPadeEffect()
    {
        Button_Start.GetComponent<Image>().DOFade(0.0f, 1.0f).SetLoops(-1, LoopType.Yoyo);
    }
}
