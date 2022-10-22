using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using GooglePlayGames;


public class GoogleTest : MonoBehaviour
{
    public TMP_Text _TMP_Google = null;

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void Login()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    _TMP_Google.text = $"{Social.localUser.id} \n {Social.localUser.userName}";
                }
                else
                {
                    _TMP_Google.text = "Login Failed";
                }
            });
        }
    }

    public void Logout()
    {
        PlayGamesPlatform platform = Social.Active as PlayGamesPlatform;
    }
}
