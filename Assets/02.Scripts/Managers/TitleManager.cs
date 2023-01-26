using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public Image Logo;
    public Button Button_Start;

    void Start()
    {
        // 해상도 고정
        SetResolution();

        Application.targetFrameRate = 120;
        Logo = GameObject.Find("Logo").GetComponent<Image>();
        Button_Start = GameObject.Find("Button_Start").GetComponent<Button>();
        Button_Start.onClick.AddListener(Button_Start_Clicked);

        Sequence LogoSequence = DOTween.Sequence().SetAutoKill(false).OnStart(() =>
        {
            Logo.transform.localScale = Vector3.zero;
            Logo.GetComponent<CanvasGroup>().alpha = 0;
        }).Append(Logo.transform.DOScale(1, 1).SetEase(Ease.OutBounce)).Join(GetComponent<CanvasGroup>().DOFade(1, 1)).SetDelay(0.5f);

        LogoSequence.Play();
        Invoke("ButtonPadeEffect", 0.5f);
        DontDestroyOnLoad(this.gameObject);
    }

    public void ButtonPadeEffect()
    {
        Button_Start.gameObject.SetActive(true);
        Button_Start.GetComponent<Image>().DOFade(0.0f, 1.0f).SetLoops(-1, LoopType.Yoyo);
    }
    public void Button_Start_Clicked()
    {
        SoundManager.Instance.PlaySFXSound("UIClick");
        SceneManager.LoadScene("Lobby");
    }

    public void SetResolution()
    {
        int setWidth = 1280; // 사용자 설정 너비
        int setHeight = 720; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
}