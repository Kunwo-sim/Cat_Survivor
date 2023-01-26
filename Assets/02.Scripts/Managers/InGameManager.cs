using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    void Awake()
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
