using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _timeText;
    float WaveRemainTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WaveRemainTime -= Time.deltaTime;
        _timeText.text = "남은 시간 : " + (int)WaveRemainTime;

        if (WaveRemainTime < 0.0f)
        {
            WaveRemainTime = 5.0f;
            Time.timeScale = 0.0f;
            UIManager.Instance.ShowPopupUI<UI_WaveShop>("UI_WaveShop");
        }
    }
}
