using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_InGame : UI_Scene
{
    enum GameObjects
    {
        JoyStick,
        HpBar,
        ExpBar,
        MoneyUI,
        Clock,
        WaveInfo,
        TimeText,
        PauseButton,
        GameOverPanel
    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.GameOverPanel).BindEvent(Gameover);
        Get<GameObject>((int)GameObjects.GameOverPanel).SetActive(false);
    }

    public void Gameover(PointerEventData data)
    {
        SceneManager.LoadScene("Lobby");
    }
}
