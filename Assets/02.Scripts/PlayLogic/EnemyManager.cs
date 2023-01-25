using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyManager>();
                if (instance == null)
                {
                    Debug.LogError("EnemyManager Instance Init Failed");
                }
            }

            return instance;
        }
    }

    private Transform _playerTransform;

    [SerializeField] private List<EnemySpawner> enemyList = new List<EnemySpawner>();

    private void Awake()
    {
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        foreach (EnemySpawner enemy in enemyList)
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
        foreach (EnemySpawner enemy in enemyList)
        {
            bool coolDownComplete = (Time.time > enemy.NextSpawnTime);
            if (coolDownComplete)
            {
                SpawnEnemy(enemy);
            }
        }
    }

    private void SpawnEnemy(EnemySpawner enemySpawner)
    {
        enemySpawner.Spawn(_playerTransform);
    }

    public void DeleteAllEnemy()
    {
    }
}