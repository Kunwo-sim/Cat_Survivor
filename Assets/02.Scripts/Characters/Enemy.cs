using UnityEngine;
using PlayLogic;

namespace Characters
{
    [CreateAssetMenu(fileName = "Enemy1", menuName = "Enemy/Enemy1")]
    public class Enemy : ScriptableObject
    {
        [SerializeField] private new string name = "Character Name";
        [SerializeField] private int hp = 10;
        [SerializeField] private int power = 1;
        [SerializeField] private float moveSpeed = 3.0f;
        [SerializeField] private int level = 1;
        [SerializeField] private float baseSpawnTime = 0.5f;
        public float NextSpawnTime { get; set; } = 0f;

        [SerializeField] private EPoolObjectType poolType = EPoolObjectType.Enemy1;

        public virtual void Spawn(Transform playerTransform)
        {
            if (ObjectPoolManager.HasObject(poolType))
            {
                NextSpawnTime = baseSpawnTime + Time.time;
                
                GameObject enemy = ObjectPoolManager.GetObject(poolType);
                enemy.transform.position = GetRandomPosition(playerTransform);
                enemy.GetComponent<EnemyObject>().Initialize(hp, power, moveSpeed, level);
            }
        }
        
        private Vector3 GetRandomPosition(Transform playerTransform)
        {
            // 생성 위치 관련 수정 필요
            float radius = Random.Range(19f, 20f);
            
            Vector3 playerPosition = playerTransform.position;
            float x = Random.Range(-radius + playerPosition.x, radius + playerPosition.x);
            float y = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - playerPosition.x, 2)) + playerPosition.y;
            y *= Random.Range(0, 2) == 0 ? -1 : 1;
 
            Vector3 randomPosition = new Vector3(x, y, 0);
            return randomPosition;
        }
    }
}