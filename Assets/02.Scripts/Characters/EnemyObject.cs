using System;
using PlayLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters
{
    public class EnemyObject : CharacterObject
    {
        private PlayerObject _playerObject;

        // Test
        public GameObject expGameObject;
        
        private void FixedUpdate()
        {
            Move(GetDirection(_playerObject.transform));
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
            _playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerObject>();
            
            // Test code
            MoveSpeed = Random.Range(1.0f, 4.0f);
        }
        protected override void Death()
        {
            base.Death();
            // 오브젝트 풀 예정
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Player"))
            {
                _playerObject.ReceiveDamage(Power);
            }
        }
    }
}
