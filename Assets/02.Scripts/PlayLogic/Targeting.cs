using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targeting
{
    private List<GameObject> GetEnemyList()
    {
        List<PoolInfo> poolList = ObjectPoolManager.GetPoolList();
        List<GameObject> objectList = new List<GameObject>();
        foreach (var pool in poolList)
        {
            // 리팩토링 필요
            if (pool.type is EPoolObjectType.Enemy1 or EPoolObjectType.Enemy2)
            {
                objectList.AddRange(pool.usingList);
            }
        }
        return objectList;
    }
    private List<GameObject> SortByDistance(Vector3 spawnPos, List<GameObject> list)
    {
        return list.OrderBy(x => Vector3.Distance(x.transform.position, spawnPos)).ToList();
    }

    private Vector3 GetNearPosition(Vector3 spawnPos)
    {
        List<GameObject> enemyList = GetEnemyList();
        if (enemyList.Count == 0) return Vector3.zero;
        enemyList = SortByDistance(spawnPos, enemyList);
        return enemyList[0].transform.position;
    }
    private Vector3 GetDirection(Vector3 pivot, Vector3 target)
    {
        Vector3 dir = target - pivot;
        dir.z = 0;
        return dir.normalized;
    }

    public Quaternion DirectToRotate(Vector3 dir)
    {
        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rot - 90);
    }
    public Vector3 GetToNearDirection(Vector3 holderPos)
    {
        Vector3 targetPos = GetNearPosition(holderPos);
        Vector3 dir = GetDirection(holderPos, targetPos);
        return dir;
    }
    public Quaternion GetToNearRotate(Vector3 holderPos)
    {
        Vector3 dir = GetToNearDirection(holderPos);
        Quaternion rot = DirectToRotate(dir);
        return rot;
    }
    public Vector3 GetToNearPosition(Vector3 dir, float distance = 3)
    {
        Vector3 pos = dir * distance;
        return pos;
    }
}