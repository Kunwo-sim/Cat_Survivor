using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    } 

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    [SerializeField] private AudioClip menuBgmClip;
    [SerializeField] private AudioClip inGameBgmClip;
    [SerializeField] private AudioClip[] sfxAudioClips;

    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        bgmPlayer = GetComponents<AudioSource>()[0];
        sfxPlayer = GetComponents<AudioSource>()[1];

        foreach (AudioClip audioClip in sfxAudioClips)
        {
            audioClipsDic.Add(audioClip.name, audioClip);
        }
    }

    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }

    public void PlayBGMSound(float volume = 1f)
    {
        bgmPlayer.loop = true; 
        bgmPlayer.volume = volume * masterVolumeBGM;

        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            bgmPlayer.clip = menuBgmClip;
            bgmPlayer.Play();
        }
        else if (SceneManager.GetActiveScene().name == "InGame")
        {
            bgmPlayer.clip = inGameBgmClip;
            bgmPlayer.Play();
        }
    }
}