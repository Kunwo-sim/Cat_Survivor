using System;
using PlayLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters
{
    public class Enemy : Character
    {
        private Player _player;
        private EPoolObjectType _poolType;

        // Test
        public GameObject expGameObject;
        
        
        public void Initialize(int hp, int power, float moveSpeed, int level, EPoolObjectType poolType)
        {
            Initialize();
            Hp = MaxHp = hp;
            Power = power;
            MoveSpeed = moveSpeed;
            Level = level;
            _poolType = poolType;
        }
        
        private void FixedUpdate()
        {
            Move(GetDirection(_player.transform));
        }
        
        private Vector2 GetDirection(Transform target)
        {
            return target.position - transform.position;
        }

        private void CreatExpObject()
        {
            // 오브젝트 풀 예정
            Instantiate(expGameObject, transform);
            expGameObject.GetComponent<ExpObject>().Initialize(Level);
        }

        protected override void Awake()
        {
            base.Awake();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            
            // Test code
            MoveSpeed = Random.Range(1.0f, 4.0f);
        }
        protected override void Death()
        {
            base.Death();
            ObjectPoolManager.ReturnObject(gameObject, _poolType);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Player"))
            {
                _player.ReceiveDamage(Power);
            }
        }
    }
}
