using System.Collections;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private new string name = "Character Name";
    [SerializeField] private int hp = 10;
    [SerializeField] private int power = 1;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private int level = 1;

    [SerializeField] private EPoolObjectType poolType = EPoolObjectType.Enemy_Mouse;

    public void Spawn(Vector3 spawnPos)
    {
        if (ObjectPoolManager.HasObject(poolType))
        {
            float randSpeed = Random.Range(-0.7f, 0.7f);
            GameObject enemy = ObjectPoolManager.GetObject(poolType);
            enemy.transform.position = spawnPos;
            enemy.GetComponent<Enemy>().Initialize(hp, power, moveSpeed + randSpeed, level, poolType);
        }
    }

}