using Characters;
using UnityEngine;

namespace PlayLogic
{
    public class ExpObject : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Player _player;
        private float _exp;
        private readonly EPoolObjectType _poolType = EPoolObjectType.ExpObject;
        
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }

        public void Initialize(float exp, Vector3 position)
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
            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _player.ReceiveExp(_exp);
                ObjectPoolManager.ReturnObject(gameObject,_poolType);
            }
        }
    }
}