using UI;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterObject : MonoBehaviour
    {
        private string _name = "Character Name";
        private int _maxHp = 10;
        private int _hp = 10;
        protected int Power = 1;
        protected float MoveSpeed = 3.0f;
        protected int Level = 1;
        private readonly float _protectionTime = 0.1f;
        private float _lastProtectionTime = 0;
        private bool _isAlive = true;
        private HpBar _hpBar;

        public void Initialize()
        {
            _isAlive = true;
            _lastProtectionTime = 0;
            _hp = _maxHp;
        }
        public void Initialize(int hp, int power, float moveSpeed, int level)
        {
            Initialize();
            _hp = _maxHp = hp;
            Power = power;
            MoveSpeed = moveSpeed;
            Level = level;
        }

        protected virtual void Awake()
        {
            Initialize();
            _hpBar = GetComponentInChildren<HpBar>();
        }

        protected void Move(Vector2 input)
        {
            if (_isAlive)
            {
                Vector2 dir = input.normalized;
                transform.Translate(dir * (MoveSpeed * 0.03f));
            }
        }

        protected virtual void Death()
        {
            _hp = 0;
            _isAlive = false;
        }

        protected void SetHpUI()
        {
            _hpBar.SetHpBar(_maxHp, _hp);
        }
        public virtual void ReceiveDamage(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                Death();
            }
        }
    }
}