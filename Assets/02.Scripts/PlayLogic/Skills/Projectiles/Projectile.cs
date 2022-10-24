using Characters;
using UnityEngine;

namespace PlayLogic
{
    public class Projectile : MonoBehaviour
    {
        private int _damage;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public void Initialize(int damage, Vector2 spawnPos, Quaternion spawnRot, float activeTime, Vector2 force)
        {
            this._damage = damage;
            transform.SetPositionAndRotation(spawnPos, spawnRot);
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            Invoke(nameof(DeleteProjectile), activeTime);
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<Enemy>().ReceiveDamage(_damage);
                DeleteProjectile();
            }
        }

        protected void DeleteProjectile()
        {
            // 오브젝트 풀링 예정
            Destroy(gameObject);
        }
    }
}