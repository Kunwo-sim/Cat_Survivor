using UnityEngine;
using Characters;
using System.Collections.Generic;

namespace PlayLogic
{
    public class EnemyManager : MonoBehaviour
    {
        private Transform _playerTransform;

        [SerializeField]
        private List<Enemy> enemyList = new List<Enemy>();
        
        private void Awake()
        {
            _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
            foreach (Enemy enemy in enemyList)
            {
                enemy.NextSpawnTime = 0;
            }
        }
        
        private void Update()
        {
            CheckSpawnTime();
        }
        
        private void CheckSpawnTime()
        {
            foreach (Enemy enemy in enemyList)
            {
                bool coolDownComplete = (Time.time > enemy.NextSpawnTime);
                if (coolDownComplete)
                {
                    SpawnEnemy(enemy);
                }
            }
        }
        
        private void SpawnEnemy(Enemy enemy)
        {
            enemy.Spawn(_playerTransform);
        }
    }
}
