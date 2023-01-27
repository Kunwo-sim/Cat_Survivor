using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using static Define;
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
                if (WaveManager.Instance._bWaveEnd) yield break;
                float randX = Random.Range(-randRange, randRange);
                float randY = Random.Range(-randRange*0.7f, randRange*0.7f);
                groupSpawnPos += new Vector3(randX, randY);
                spawnInfo.EnemyInfo.Spawn(groupSpawnPos);
                yield return new WaitForSeconds(spawnInfo.IntervalTime);
            }
            yield return new WaitForSeconds(spawnInfo.RepeatTime);
        }
    }
    private void Stage1Wave()
    {
        
        _stageWave.Add(new SpawnInfo(enemyList[BossSheep], GetRandomPosition, 1, 0.1f, 5, 1, 5));

        // Wave 1
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 10, 0.5f, 3, 1, 40));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 5, 0.2f, 5, 1, 40));
        
        float endTime = 2.1f;
        // Wave 2
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 6, 0.2f, 5, 40 + endTime, 60+ endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 3, 1f, 3, 40 + endTime, 60+ endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 4, 0.8f, 3, 40 + endTime, 60+ endTime));
        endTime += 2;
        // Wave 3
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 8, 0.2f, 3, 60 + endTime, 100+ endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 5, 0.2f, 5, 60+ endTime, 100+ endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Sheep], GetRandomPosition, 3, 0.2f, 4, 75+ endTime, 100+ endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Boar], GetRandomPosition, 4, 0.4f, 3, 75+ endTime, 100+ endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 3, 0.2f, 2, 75+ endTime, 100+ endTime));
        endTime += 2;
        // Wave 4
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 12, 0.2f, 6, 100+ endTime, 120+ endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Sheep], GetRandomPosition, 3, 0.3f, 2, 100+ endTime, 120+ endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Boar], GetRandomPosition, 5, 0.4f, 2, 100+ endTime, 120+ endTime));
        endTime += 2;
        // Wave 5 보스
        _stageWave.Add(new SpawnInfo(enemyList[BossSheep], GetRandomPosition, 1, 0.1f, 5, 121+endTime, 122+endTime));
    }
    
    private Vector3 GetRandomPosition()
    {
        float xPos = Random.Range(-xSpawnLimit + randRange, xSpawnLimit - randRange);
        float yPos = Random.Range(-ySpawnLimit + randRange*0.7f, ySpawnLimit - randRange*0.7f);
        float zPos = 10;

        var randomPosition = new Vector3(xPos, yPos, zPos);
        return randomPosition;
    }
    private Vector3 GetSidePosition()
    {
        Vector3 randomPosition = Vector3.zero;
        float yPos = ySpawnLimit - randRange;
        float xPos = xSpawnLimit- randRange;
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