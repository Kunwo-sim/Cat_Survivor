using UnityEngine;

[CreateAssetMenu(fileName = "Enemy1", menuName = "Enemy/Enemy1")]
public class EnemyInfo : ScriptableObject
{
    [SerializeField] private new string name = "Character Name";
    [SerializeField] private int hp = 10;
    [SerializeField] private int power = 1;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private int level = 1;
    [SerializeField] private float baseSpawnTime = 0.5f;
    public float NextSpawnTime { get; set; } = 0f;

    [SerializeField] private EPoolObjectType poolType = EPoolObjectType.Enemy1;

    public void Initialize()
    {
        NextSpawnTime = baseSpawnTime;
    }

    public void Spawn(Transform playerTransform)
    {
        if (ObjectPoolManager.HasObject(poolType))
        {
            NextSpawnTime = baseSpawnTime + Time.time;

            GameObject enemy = ObjectPoolManager.GetObject(poolType);
            enemy.transform.position = GetRandomPosition(playerTransform);
            enemy.GetComponent<Enemy>().Initialize(hp, power, moveSpeed, level, poolType);
        }
    }

    private Vector3 GetRandomPosition(Transform playerTransform)
    {

        Vector3 randomPosition = Vector3.zero;
        float xPos = Random.Range(-Define.xSpawnLimit, Define.xSpawnLimit);
        float yPos = Random.Range(-Define.ySpawnLimit, Define.ySpawnLimit);
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