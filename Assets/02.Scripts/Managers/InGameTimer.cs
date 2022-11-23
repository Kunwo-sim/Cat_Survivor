using System;
using UnityEngine;

public class InGameTimer : MonoBehaviour
{
    private float _time;
    private readonly float _endTime = 60 * 5;
    private Bar _timeBar;
    private void Awake()
    {
        _timeBar = GameObject.Find("Time Bar").GetComponent<Bar>();
    }
    private void Start()
    {
        _time = 0;
        _timeBar.SetBar(_endTime, _time);
    }
    private void Update()
    {
        SetTime();
    }

    private void SetTime()
    {
        _time += Time.deltaTime;
        _timeBar.SetBar(_endTime, _time);
    }
}