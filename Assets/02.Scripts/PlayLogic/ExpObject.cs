using Characters;
using UnityEngine;

namespace PlayLogic
{
    public class ExpObject : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private PlayerObject _playerObject;
        private float _exp;

        // Test
        public float TestExp;
        
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerObject>();
            
            // Test
            Initialize(TestExp);
        }

        public void Initialize(float exp)
        {
            _exp = exp;
            float size = _exp * 0.02f + 0.3f;
            transform.localScale = new Vector3(size, size, 1);
            if (_exp < 5)
            {
                _renderer.color = Color.white;
            }
            else if (_exp < 10)
            {
                _renderer.color = Color.green;
            }
            else
            {
                _renderer.color = Color.yellow;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _playerObject.ReceiveExp(_exp);
                // 오브젝트 풀 필요
                Destroy(gameObject);
            }
        }
    }
}