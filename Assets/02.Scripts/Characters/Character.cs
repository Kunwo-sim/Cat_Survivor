using UI;
using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        private string _name = "Character Name";
        protected float MaxHp = 10;
        protected float Hp = 10;
        protected float Power = 1;
        protected float MoveSpeed = 3.0f;
        protected int Level = 1;
        private readonly float _protectionTime = 0.1f;
        private float _lastProtectionTime = 0;
        private bool _isAlive = true;
        private Bar _hpBar;

        public void Initialize()
        {
            _isAlive = true;
            _lastProtectionTime = 0;
            Hp = MaxHp;
        }

        protected virtual void Awake()
        {
            _hpBar = GetComponentInChildren<Bar>();
        }
        
        protected virtual void Start()
        {
            Initialize();
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
            Hp = 0;
            _isAlive = false;
        }

        protected void SetHpUI()
        {
            _hpBar.SetBar(MaxHp, Hp);
            _hpBar.SetText(Mathf.FloorToInt(Hp));
        }
        public virtual void ReceiveDamage(float damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Death();
            }
        }
    }
}