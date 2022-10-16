using UnityEngine;

namespace Characters
{
    public class Enemy : Character
    {
        private Transform _playerTransform;

        protected override void Awake()
        {
            base.Awake();
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            Move(GetDirection(_playerTransform));
        }

        private Vector2 GetDirection(Transform target)
        {
            return target.position - transform.position;
        }
    
        protected override void Death()
        {
            base.Death();
            // ObjectPool Queue에 반환
        }
    }
}
