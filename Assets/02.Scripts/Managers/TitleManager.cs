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


    void Start()
    {
        Application.targetFrameRate = 120;
        Text_Title = GameObject.Find("Text_Title").GetComponent<TextMeshProUGUI>();
        Button_Start = GameObject.Find("Button_Start").GetComponent<Button>();
        Button_Start.onClick.AddListener(Button_Start_Clicked);

        TitleFadeEffect();
        ButtonPadeEffect();
        DontDestroyOnLoad(this.gameObject);
    }

    public void TitleFadeEffect()
    {
        Text_Title.DOFade(0.0f, 1.0f).SetLoops(-1, LoopType.Yoyo);
    }

    public void ButtonPadeEffect()
    {
        Button_Start.GetComponent<Image>().DOFade(0.0f, 1.0f).SetLoops(-1, LoopType.Yoyo);
    }
    public void Button_Start_Clicked()
    {
        SceneManager.LoadScene("Lobby");
    }
}