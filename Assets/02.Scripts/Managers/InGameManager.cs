using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InGameManager>();
                if (instance == null)
                {
                    Debug.LogError("InGameManager Instance Init Failed");
                }
            }

            return instance;
        }
    }

    int _money = 0;

    [SerializeField]
    GameObject _moneyUI;

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            _moneyUI.GetComponentInChildren<TextMeshProUGUI>().text = _money.ToString();
        }
    }

    void Awake()
    {
        SetFixedCanvas();
    }

    void Start()
    {
        Money = 0;
    }

    void SetFixedCanvas()
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
