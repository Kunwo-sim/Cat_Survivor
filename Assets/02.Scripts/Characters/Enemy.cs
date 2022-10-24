using UnityEngine;

namespace Characters
{
    public class Enemy : Character
    {
        private Transform _playerTransform;

        
        private void Update()
        {
            Move(GetDirection(_playerTransform));
        }
        
        private Vector2 GetDirection(Transform target)
        {
            return target.position - transform.position;
        }
        
        protected override void Awake()
        {
            base.Awake();
            _playerTransform = GameObject.FindWithTag("Player").transform;
            
            // Test code
            MoveSpeed = Random.Range(1.0f, 4.0f);
        }
        
        protected override void Death()
        {
            base.Death();
            // 오브젝트 풀링 예정
            Destroy(gameObject);
        }
    
    }
}
