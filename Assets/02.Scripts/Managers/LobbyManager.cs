using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGMSound();
    }
    public void Button_GameStart_Clicked()
    {
        CatSceneManager.Instance.ChangeScene("InGame");
    }
}
