using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField]
    private float _xOffest = 8;
    [SerializeField]
    private float _yOffest = 6;

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

        float targetX = playerPos.x;
        float targetY = playerPos.y;

        if (targetX > _xOffest)
            targetX = _xOffest;
        else if (targetX < -_xOffest)
            targetX = -_xOffest;

        if (targetY > _yOffest)
            targetY = _yOffest;
        else if (targetY < -_yOffest)
            targetY = -_yOffest;

        Vector3 targetPos = new Vector3(targetX, targetY, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, camToPlayerSpeed);
    }
}
