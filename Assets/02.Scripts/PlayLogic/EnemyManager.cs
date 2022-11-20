using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField] private List<EnemyInfo> enemyList = new List<EnemyInfo>();

    private void Awake()
    {
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        foreach (EnemyInfo enemy in enemyList)
        {
            enemy.Initialize();
        }
    }

    private void Update()
    {
        CheckSpawnTime();
    }

    private void CheckSpawnTime()
    {
        foreach (EnemyInfo enemy in enemyList)
        {
            bool coolDownComplete = (Time.time > enemy.NextSpawnTime);
            if (coolDownComplete)
            {
                SpawnEnemy(enemy);
            }
        }
    }

    private void SpawnEnemy(EnemyInfo enemyInfo)
    {
        enemyInfo.Spawn(_playerTransform);
    }
}