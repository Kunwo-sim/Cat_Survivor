using System;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트 구분을 위한 타입
public enum EPoolObjectType
{
    Skill1,
    Skill2,
    Enemy1,
    Enemy2,
    ExpObject
}
// 오브젝트 풀
[Serializable]
public class PoolInfo
{
    public EPoolObjectType type;
    public int initCount;
    public GameObject prefab;
    public GameObject container;

    public Queue<GameObject> poolQueue = new Queue<GameObject>();
    // 오브젝트 전체 회수를 위한 큐
    public List<GameObject> poolDeQueue = new List<GameObject>();
}
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    // 오브젝트 풀 리스트
    [SerializeField] private List<PoolInfo> poolInfoList;

    private void Awake()
    {
        Instance = this;
        Initialize();
    }

    // 각 풀마다 정해진 개수의 오브젝트를 생성해주는 초기화 함수 
    private void Initialize()
    {
        foreach (PoolInfo poolInfo in poolInfoList)
        {
            for (int i = 0; i < poolInfo.initCount; i++)
            {
                poolInfo.poolQueue.Enqueue(CreatNewObject(poolInfo));
            }
        }
    }

    // 초기화 및 풀에 오브젝트가 부족할 때 오브젝트를 생성하는 함수
    private GameObject CreatNewObject(PoolInfo poolInfo)
    {
        GameObject newObject = Instantiate(poolInfo.prefab, poolInfo.container.transform);
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    // ObjectType(Enum)으로 해당하는 PoolInfo를 반환해주는 함수
    private PoolInfo GetPoolByType(EPoolObjectType type)
    {
        foreach (PoolInfo poolInfo in poolInfoList)
        {
            if (type == poolInfo.type)
            {
                return poolInfo;
            }
        }
        return null;
    }

    // 오브젝트가 필요할 때 호출하는 함수
    public static GameObject GetObject(EPoolObjectType type)
    {
        PoolInfo poolInfo = Instance.GetPoolByType(type);
        GameObject objInstance = null;
        if (HasObject(type))
        {
            poolInfo.poolDeQueue.Add(poolInfo.poolQueue.Dequeue());
            objInstance = poolInfo.poolDeQueue[^1];
            
        }
        else
        {
            objInstance = Instance.CreatNewObject(poolInfo);
        }
        objInstance.SetActive(true);
        return objInstance;
    }

    // 오브젝트 사용 후 다시 풀에 반환하는 함수
    public static void ReturnObject(GameObject obj, EPoolObjectType type)
    {
        PoolInfo poolInfo = Instance.GetPoolByType(type);
        poolInfo.poolQueue.Enqueue(obj);
        poolInfo.poolDeQueue.Remove(obj);
        obj.SetActive(false);
    }

    // 매개변수 type인 오브젝트 전체 반환
    public static void ReturnObjectAll(EPoolObjectType type)
    {
        PoolInfo poolInfo = Instance.GetPoolByType(type);
        while (poolInfo.poolDeQueue.Count > 0)
        {
            ReturnObject(poolInfo.poolDeQueue[0], type);
        }
    }
    
    public static bool HasObject(EPoolObjectType type)
    {
        PoolInfo poolInfo = Instance.GetPoolByType(type);
        bool hasObject = poolInfo.poolQueue.Count != 0;

        return hasObject;
    }
    
    
}