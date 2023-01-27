using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    private static WaveManager instance;
    public static WaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WaveManager>();
                if (instance == null)
                {
                    Debug.LogError("WaveManager Instance Init Failed");
                }
            }

            return instance;
        }
    }

    float _waveRemainTime = 20.0f;
    public int _waveStep = 1;
    public bool _bWaveEnd = false;

    [SerializeField]
    TextMeshProUGUI _waveInfoText;
    [SerializeField]
    TextMeshProUGUI _timeText;
    [SerializeField]
    GameObject _expParent;
    Player _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_bWaveEnd)
            return;

        _waveRemainTime -= Time.deltaTime;

        if (_waveRemainTime < 0.0f)
        {
            _bWaveEnd = true;
            _waveRemainTime = 0.0f;
            

            foreach(Transform expChild in _expParent.transform)
            {
                expChild.GetComponent<ExpObject>()._bToUI = true;
            }
            ReturnAllEnemy();

            Invoke("WaveEnded", 2f);
        }

        _timeText.text = "남은 시간 : " + (int)_waveRemainTime;
    }

    private void ReturnAllEnemy()
    {
        // 리팩토링 필요
        ObjectPoolManager.ReturnObjectAll(EPoolObjectType.Enemy_Mouse);
        ObjectPoolManager.ReturnObjectAll(EPoolObjectType.Enemy_Boar);
        ObjectPoolManager.ReturnObjectAll(EPoolObjectType.Enemy_Snake);
        ObjectPoolManager.ReturnObjectAll(EPoolObjectType.Enemy_Sheep);
        ObjectPoolManager.ReturnObjectAll(EPoolObjectType.EnemyProjectile);
    }
    
    public void WaveEnded()
    {
        Time.timeScale = 0.0f;
        if (_player.LevelCnt > 0)
        {
            _player.LevelCnt -= 1;
            UIManager.Instance.ShowPopupUI<UI_AbilityUpgrade>("UI_AbilityUpgrade");
        }
        else
        {
            UIManager.Instance.ShowPopupUI<UI_WaveShop>("UI_WaveShop");
        }
    }

    public void BeforeWaveStart()
    {
        _player.transform.position = Vector3.zero;
        _bWaveEnd = false;
        _waveStep += 1;
        _player.Hp = _player.MaxHp;
        _waveRemainTime = 20.0f;
        Time.timeScale = 1.0f;
        _waveInfoText.text = _waveStep.ToString();
    }

    public void SetWaveTime(float value)
    {
        _waveRemainTime = value;
    }
}
