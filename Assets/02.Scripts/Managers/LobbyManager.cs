using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void Button_GameStart_Clicked()
    {
        SceneManager.LoadScene("InGame");
    }
}
