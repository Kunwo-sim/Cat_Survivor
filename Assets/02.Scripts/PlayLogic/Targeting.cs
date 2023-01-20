using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Targeting
{
    public static List<GameObject> GetEnemyList()
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
    public static List<GameObject> SortByDistance(Vector3 spawnPos, List<GameObject> list)
    {
        return list.OrderBy(x => Vector3.Distance(x.transform.position, spawnPos)).ToList();
    }

    public static GameObject GetNearGameObject(Vector3 spawnPos)
    {
        List<GameObject> enemyList = GetEnemyList();
        if (enemyList.Count == 0) return null;
        enemyList = SortByDistance(spawnPos, enemyList);
        return enemyList[0];
    }
    
    public static Quaternion GetLookAtQuaternion(Vector3 dir)
    {
        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rot - 90);
    }
    public static Vector3 GetDirection(Vector3 pivot, Vector3 target)
    {
        Vector3 dir = target - pivot;
        return dir.normalized;
    }

}
