using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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

    private delegate Vector3 SpawnType();
    public delegate void Stage();
    [SerializeField] private List<EnemyInfo> enemyList = new List<EnemyInfo>();

  
    private List<SpawnInfo> _stageWave = new List<SpawnInfo>(); 
    private readonly float _randRange = 5f;

    private struct SpawnInfo
    {
        public EnemyInfo EnemyInfo;
        public SpawnType SpawnType;
        public int Count;
        public float IntervalTime;
        public float RepeatTime;
        public float StartTime;
        public float EndTime;
        public SpawnInfo(EnemyInfo enemyInfo, SpawnType spawnType, int count, float intervalTime, float repeatTime, float startTime, float endTime)
        {
            EnemyInfo = enemyInfo;
            SpawnType = spawnType;
            Count = count;
            IntervalTime = intervalTime;
            RepeatTime = repeatTime;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
    
    private void Start()
    {
        Stage1Wave();   
        foreach (var routine in _stageWave)
            StartCoroutine(SpawnRoutine(routine));
    }

    public void DeleteAllEnemy()
    {
    }

    private IEnumerator SpawnRoutine(SpawnInfo spawnInfo)
    {
        yield return new WaitForSeconds(spawnInfo.StartTime);
        while (Time.time < spawnInfo.EndTime)
        {
            var groupSpawnPos = spawnInfo.SpawnType();
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                float randX = Random.Range(-_randRange, _randRange);
                float randY = Random.Range(-_randRange, _randRange);
                groupSpawnPos += new Vector3(randX, randY);
                spawnInfo.EnemyInfo.Spawn(groupSpawnPos);
                yield return new WaitForSeconds(spawnInfo.IntervalTime);
            }
            yield return new WaitForSeconds(spawnInfo.RepeatTime);
        }
    }
    private void Stage1Wave()
    {
        _stageWave.Add(new SpawnInfo(enemyList[0], GetRandomPosition, 5, 0.2f, 5, 0, 40));
        _stageWave.Add(new SpawnInfo(enemyList[1], GetRandomPosition, 10, 0.5f, 3, 0, 40));
        
        // Test
        _stageWave.Add(new SpawnInfo(enemyList[2], GetRandomPosition, 3, 0.5f, 2, 0, 40));
        _stageWave.Add(new SpawnInfo(enemyList[3], GetRandomPosition, 3, 0.5f, 2, 0, 40));

    }
    
    private Vector3 GetRandomPosition()
    {
        float xPos = Random.Range(-Define.xSpawnLimit + _randRange, Define.xSpawnLimit - _randRange);
        float yPos = Random.Range(-Define.ySpawnLimit + _randRange, Define.ySpawnLimit - _randRange);
        float zPos = 10;

        var randomPosition = new Vector3(xPos, yPos, zPos);
        return randomPosition;
    }
    private Vector3 GetSidePosition()
    {
        Vector3 randomPosition = Vector3.zero;
        float yPos = Define.ySpawnLimit - _randRange;
        float xPos = Define.xSpawnLimit- _randRange;
        float zPos = 10;

        int flag = Random.Range(0, 4);
        switch (flag)
        {
            case 0:
                randomPosition = new Vector3(xPos, Random.Range(-yPos, yPos), zPos);
                break;
            case 1:
                randomPosition = new Vector3(-xPos, Random.Range(-yPos, yPos), zPos);
                break;
            case 2:
                randomPosition = new Vector3(Random.Range(-xPos, xPos), yPos, zPos);
                break;
            case 3:
                randomPosition = new Vector3(Random.Range(-xPos, xPos), -yPos, zPos);
                break;
        }
        return randomPosition;
    }
}