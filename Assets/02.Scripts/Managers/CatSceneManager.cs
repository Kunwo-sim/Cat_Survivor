using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CatSceneManager : MonoBehaviour
{
    float _fadeDuration = 2;

    //public GameObject _loadingObject;
    public CanvasGroup _fadeImg;
    public TextMeshProUGUI _loadingText;
    
    public static CatSceneManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static CatSceneManager instance;

    void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _fadeImg.DOFade(0, _fadeDuration)
        .OnStart(() => {
            _loadingText.gameObject.SetActive(false);
            // _loadingObject.SetActive(false);
        })
        .OnComplete(() => {
            _fadeImg.blocksRaycasts = false;
        });
    }

    public void ChangeScene(string sceneName)
    {
        _fadeImg.DOFade(1, _fadeDuration)
        .OnStart(() =>
        {
            _fadeImg.blocksRaycasts = true;
        })
        .OnComplete(() =>
        {
            StartCoroutine("LoadScene", sceneName);
        });
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator LoadScene(string sceneName)
    {
        _loadingText.gameObject.SetActive(true);
        //_loadingObject.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        float past_time = 0;
        float percentage = 0;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true;
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }
            _loadingText.text = percentage.ToString("0") + "%";
        }
    }
}