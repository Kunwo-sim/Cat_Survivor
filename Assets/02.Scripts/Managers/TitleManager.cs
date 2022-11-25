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
}