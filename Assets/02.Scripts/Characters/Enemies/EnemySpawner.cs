using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private new string name = "Character Name";
    [SerializeField] private int hp = 10;
    [SerializeField] private int power = 1;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private int level = 1;
    [SerializeField] private float baseSpawnTime = 0.5f;
    private readonly float _randRange = 7f;
    public float NextSpawnTime { get; set; } = 0f;

    [SerializeField] private EPoolObjectType poolType = EPoolObjectType.Enemy1;

    public void Initialize()
    {
        NextSpawnTime = baseSpawnTime;
    }

    private void Spawn(Vector3 spawnPos)
    {
        if (ObjectPoolManager.HasObject(poolType))
        {
            GameObject enemy = ObjectPoolManager.GetObject(poolType);
            enemy.transform.position = spawnPos;
            enemy.GetComponent<Enemy>().Initialize(hp, power, moveSpeed, level, poolType);
        }
    }

    public IEnumerator SpawnPattern(int count)
    {
        NextSpawnTime = baseSpawnTime + Time.time;
        Vector3 groupSpawnPos = GetRandomPosition();
        for (int i = 0; i < count; i++)
        {
            float randX = Random.Range(-_randRange, _randRange);
            float randY = Random.Range(-_randRange, _randRange);
            groupSpawnPos += new Vector3(randX, randY);
            Spawn(groupSpawnPos);
            yield return new WaitForSeconds(0.15f);
        }
        yield break;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        float xPos = Random.Range(-Define.xSpawnLimit + _randRange, Define.xSpawnLimit - _randRange);
        float yPos = Random.Range(-Define.ySpawnLimit + _randRange, Define.ySpawnLimit - _randRange);
        float zPos = 10;

        // 소환 로직 수정
        //int flag = Random.Range(0, 4);
        //switch (flag)
        //{
        //    case 0:
        //        randomPosition = new Vector3(max, Random.Range(min, max), zPos);
        //        break;
        //    case 1:
        //        randomPosition = new Vector3(min, Random.Range(min, max), zPos);
        //        break;
        //    case 2:
        //        randomPosition = new Vector3(Random.Range(min, max), max, zPos);
        //        break;
        //    case 3:
        //        randomPosition = new Vector3(Random.Range(min, max), min, zPos);
        //        break;
        //}
        randomPosition = new Vector3(xPos, yPos, zPos);
        return randomPosition;
    }
}