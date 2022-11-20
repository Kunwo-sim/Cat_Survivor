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
        _TMP_Google.text = "로그인 시도";
        if (PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            _TMP_Google.text = "로그인 중...";
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    _TMP_Google.text = $"{Social.localUser.id} \n {Social.localUser.userName}";
                }
                else
                {
                    _TMP_Google.text = "로그인 실패";
                }
            });
            _TMP_Google.text = "로그인 끝!";
        }
        else
        {
            _TMP_Google.text = "이미 로그인 됨";
        }
    }

    public void Logout()
    {
        PlayGamesPlatform platform = Social.Active as PlayGamesPlatform;
    }
}
