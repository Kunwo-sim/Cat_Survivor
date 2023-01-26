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
        // �ػ� ����
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
        int setWidth = 1280; // ����� ���� �ʺ�
        int setHeight = 720; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }
}