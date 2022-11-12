using PlayLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayLogic
{
    public class EnemyManager : MonoBehaviour
    {
        private Transform _playerTransform;

        private void Awake()
        {
            _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), 0f, 0.5f);
            InvokeRepeating(nameof(SpawnEnemy), 0f, 0.5f);
        }

        private void SpawnEnemy()
        {
            // Enemy ScriptableObject화에 따른 확장 구조화 필요
            GameObject enemy = ObjectPoolManager.GetObject(EPoolObjectType.Enemy1);
            enemy.transform.position = GetRandomPosition();
        }
    
        public Vector3 GetRandomPosition()
        {
            // 생성 위치 관련 수정 필요
            float radius = Random.Range(23f, 25f);
            
            Vector3 playerPosition = _playerTransform.position;
            float x = Random.Range(-radius + playerPosition.x, radius + playerPosition.x);
            float y = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - playerPosition.x, 2)) + playerPosition.y;
            y *= Random.Range(0, 2) == 0 ? -1 : 1;
 
            Vector3 randomPosition = new Vector3(x, y, 0);
            return randomPosition;
        }
    }
}
