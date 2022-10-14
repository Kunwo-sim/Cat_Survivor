using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private string _name;
    private int _maxHp;
    private int _hp;
    private int _power;
    private int _level;
    private float _moveSpeed;
    private float _protectionTime;
    private float _lastProtectionTime;
    private bool _isAlive;
    
    private Vector3 _moveDelta;

    protected void Initialize()
    {
        
    }

    protected void Move(Vector3 input)
    {
        
    }

    protected abstract void Death();
}
