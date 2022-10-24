using System;
using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        // 구조체로?
        private string _name = "Character Name";
        private int _maxHp = 10;
        private int _hp = 10;
        private int _power = 1;
        protected float MoveSpeed = 3.0f;
        private int _level = 1;
        private readonly float _protectionTime = 0.1f;
        private float _lastProtectionTime = 0;
        private bool _isAlive = true;

        private void Initialize()
        {
            _isAlive = true;
            _lastProtectionTime = 0;
            
            // 어떤 방법으로 (ScriptableObject, DataTable 등)
            // 초기화 해야할지 고민중 . . . 
        }
        
        protected virtual void Awake()
        {
            Initialize();
        }
        
        protected void Move(Vector2 input)
        {
            if (_isAlive)
            {
                Vector2 dir = input.normalized;
                transform.Translate(dir * (MoveSpeed * Time.deltaTime));
            }
        }
        
        protected virtual void Death()
        {
            _hp = 0;
            _isAlive = false;
        }
        
        public void ReceiveDamage(int damage)
        {
            _hp -= damage;
            Debug.Log($"{name} hp : {_hp}");
            if (_hp <= 0)
            {
                Death();
            }
        }
    }
}
