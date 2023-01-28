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
    public int _normalAbilityPercnt = 50;
    public int _rareAbilityPercnt = 85;
    public int _epicAbilityPercnt = 97;
    public int _legendAbilityPercnt = 100;
    [SerializeField]
    public Sprite[] abilityPanelSprite;
    [SerializeField]
    public Sprite[] itemPanelSprite;
    [SerializeField]
    public Sprite[] itemIconSprite;
    [SerializeField]
    GameObject _moneyUI;

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            _moneyUI.GetComponentInChildren<TextMeshProUGUI>().text = _money.ToString();
            SoundManager.Instance.PlaySFXSound("Get_Coin");
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
