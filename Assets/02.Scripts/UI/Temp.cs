using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.ShowPopupUI<UI_Button>("UI_Button");

        UIManager.Instance.ClosePopupUI();

        UIManager.Instance.ShowSceneUI<UI_Inven>();
    }
}
