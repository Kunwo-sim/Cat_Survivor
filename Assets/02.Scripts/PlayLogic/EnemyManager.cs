using System;
using PlayLogic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
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
        GameObject enemy = ObjectPoolManager.GetObject(EPoolObjectType.Enemy1);
        enemy.transform.position = GetRandomPosition();
    }
    
    public Vector3 GetRandomPosition()
    {
        Vector3 playerPosition = _playerTransform.position;
        float radius = Random.Range(23f, 25f);
        float x = Random.Range(-radius + playerPosition.x, radius + playerPosition.x);
        float y = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - playerPosition.x, 2)) + playerPosition.y;
        y *= Random.Range(0, 2) == 0 ? -1 : 1;
 
        Vector3 randomPosition = new Vector3(x, y, 0);
        return randomPosition;
    }
}
