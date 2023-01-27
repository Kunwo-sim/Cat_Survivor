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
                float randY = Random.Range(-randRange * 0.7f, randRange * 0.7f);
                groupSpawnPos += new Vector3(randX, randY);
                spawnInfo.EnemyInfo.Spawn(groupSpawnPos);
                yield return new WaitForSeconds(spawnInfo.IntervalTime);
            }
            yield return new WaitForSeconds(spawnInfo.RepeatTime);
        }
    }
    private void Stage1Wave()
    {
        float[] waveTime = new float[11];
        for (int i = 1; i <= 10; i++)
        {
            waveTime[i] += waveTime[i - 1];
            waveTime[i] += WaveManager.Instance._waveTime[i];
        }

        // Wave 1
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 3, 0.2f, 2, 1, 20));
        float endTime = 2.2f;
        // Wave 2
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 3, 0.2f, 3, 20 + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 2, 0.2f, 3, 20 + endTime, 999 + endTime));
        endTime += 2.2f;
        // Wave 3
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 4, 0.2f, 3, 45 + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 4, 0.2f, 7, 45 + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 4, 0.2f, 5, 45 + endTime, 999 + endTime));
        endTime += 2.2f;
        // Wave 4
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 6, 0.2f, 4, 75 + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Sheep], GetRandomPosition, 1, 0.3f, 5, 75 + endTime, 999 + endTime));
        endTime += 2.2f;
        // Wave 5
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 6, 0.2f, 4, 110 + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 3, 0.2f, 7, 110 + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Sheep], GetRandomPosition, 1, 0.2f, 4, 110 + endTime, 999 + endTime));
        endTime += 2.2f;
        // Wave 6
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 3, 0.2f, 3, waveTime[5] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 6, 0.2f, 8, waveTime[5] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 4, 0.2f, 5, waveTime[5] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Sheep], GetRandomPosition, 2, 0.2f, 6, waveTime[5] + endTime, 999 + endTime));
        endTime += 2.2f;
        // Wave 7                                                                                            
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 4, 0.2f, 5, waveTime[6] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 6, 0.2f, 8, waveTime[6] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 2, 0.4f, 4, waveTime[6] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 4, 0.4f, 7, waveTime[6] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Boar], GetRandomPosition, 1, 0.4f, 6, waveTime[6] + endTime, 999 + endTime));
        endTime += 2.2f;
        // Wave 8
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 6, 0.2f, 9, waveTime[7] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Sheep], GetRandomPosition, 2, 0.2f, 5, waveTime[7] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Boar], GetRandomPosition, 2, 0.2f, 7, waveTime[7] + endTime, 999 + endTime));
        endTime += 2.2f;
        // Wave 9
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 3, 0.2f, 2, waveTime[8] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 2, 0.2f, 2, waveTime[8] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Sheep], GetRandomPosition, 3, 0.2f, 6, waveTime[8] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Boar], GetRandomPosition, 3, 0.4f, 7, waveTime[8] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Mouse], GetRandomPosition, 6, 0.2f, 8, waveTime[8] + endTime, 999 + endTime));
        _stageWave.Add(new SpawnInfo(enemyList[Snake], GetRandomPosition, 4, 0.2f, 5, waveTime[8] + endTime, 999 + endTime));
        endTime += 2.2f;
        // Wave 10 보스
        //_stageWave.Add(new SpawnInfo(enemyList[BossSheep], GetRandomPosition, 1, 0.1f, 5, waveTime[9] + endTime, 999 + waveTime[9] + 1 + endTime));
    }

    private Vector3 GetRandomPosition()
    {
        float xPos = Random.Range(-xSpawnLimit + randRange, xSpawnLimit - randRange);
        float yPos = Random.Range(-ySpawnLimit + randRange * 0.7f, ySpawnLimit - randRange * 0.7f);
        float zPos = 10;

        var randomPosition = new Vector3(xPos, yPos, zPos);
        return randomPosition;
    }
    private Vector3 GetSidePosition()
    {
        Vector3 randomPosition = Vector3.zero;
        float yPos = ySpawnLimit - randRange;
        float xPos = xSpawnLimit - randRange;
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