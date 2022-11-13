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
        private EPoolObjectType _poolType;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyObject>().ReceiveDamage(_damage);
                Delete();
            }
        }
        
        private void Delete()
        {
            ObjectPoolManager.ReturnObject(gameObject, _poolType);
        }
        
        public void Initialize(Vector2 spawnPos, Quaternion spawnRot, int damage, float activeTime, EPoolObjectType poolType)
        {
            transform.SetPositionAndRotation(spawnPos, spawnRot);
            Rigidbody2D.velocity = Vector2.zero;
            _damage = damage;
            _poolType = poolType;
            Invoke(nameof(Delete), activeTime);
            StartCoroutine(Move());
        }

        protected abstract IEnumerator Move();
    }
}