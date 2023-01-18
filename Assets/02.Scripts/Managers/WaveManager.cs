using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _timeText;
    float WaveRemainTime = 5.0f;

    Player _player;
    // Start is called before the first frame update

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        WaveRemainTime -= Time.deltaTime;
        _timeText.text = "���� �ð� : " + (int)WaveRemainTime;

        if (WaveRemainTime < 0.0f)
        {
            WaveRemainTime = 5.0f;
            Time.timeScale = 0.0f;
            if (_player.Level > _player.CurLevel)
            {
                _player.CurLevel++;
                UIManager.Instance.ShowPopupUI<UI_AbilityUpgrade>("UI_AbilityUpgrade");
            }
            else
            {
                UIManager.Instance.ShowPopupUI<UI_WaveShop>("UI_WaveShop");
            }
        }
    }
}
