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
        
        // 가로20, 세로 10 기준 모서리에서 나오도록 수정
        int flag = Random.Range(0, 2);

        // 나중에 하드코딩 수정
        Vector3 offset = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        int shiftValue = 2;
        offset += new Vector3(shiftValue, shiftValue, 0);
        
        Vector3 playerPosition = playerTransform.position;
        Vector3 randomPosition = Vector3.zero;

        float x = 0.0f;
        float y = 0.0f;
        
        // x 경계에서 생성
        if (flag == 0)
        {
            x = playerPosition.x;
            x += Random.Range(0, 2) == 0 ? -offset.x : offset.x;
            y = Random.Range(playerPosition.y - offset.y, playerPosition.y + offset.y);
        }
        // y 경게에서 생성
        else
        {
            x = Random.Range(playerPosition.x - offset.x, playerPosition.x + offset.x);
            y = playerPosition.y;
            y += Random.Range(0, 2) == 0 ? -offset.y : offset.y;
        }

        //Debug.Log($"Player Pos : {playerPosition.x}, {playerPosition.y}");
        //Debug.Log($"Xpos : {x}, Ypos : {y}");
        randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }
}