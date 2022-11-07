using System.Collections;
using Characters;
using UnityEngine;

namespace PlayLogic
{
    public abstract class Projectile : MonoBehaviour
    {
        private int _damage;
        protected float Speed;

        protected Rigidbody2D Rigidbody2D;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<Enemy>().ReceiveDamage(_damage);
                Delete();
            }
        }
        
        private void Delete()
        {
            // 오브젝트 풀링 예정
            Destroy(gameObject);
        }
        
        public void Initialize(Vector2 spawnPos, Quaternion spawnRot, int damage, float activeTime)
        {
            transform.SetPositionAndRotation(spawnPos, spawnRot);
            Rigidbody2D.velocity = Vector2.zero;
            _damage = damage;
            Invoke(nameof(Delete), activeTime);
            StartCoroutine(Move());
        }

        protected abstract IEnumerator Move();
    }
}