using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private float camToPlayerSpeed = 0.01f;
    void Awake()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGMSound();
    }

    void FixedUpdate()
    {
        Vector3 playerPos = _playerTransform.transform.position;
        Vector3 targetPos = new Vector3(playerPos.x, playerPos.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, camToPlayerSpeed);
    }
}
