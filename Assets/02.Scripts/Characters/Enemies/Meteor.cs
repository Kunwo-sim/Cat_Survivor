using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Vector3 targetPos;
    
    void Start()
    {
        targetPos = transform.position + new Vector3(Define.xSpawnLimit, -Define.ySpawnLimit);
        Invoke(nameof(Des), 2.33f);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, 4*Time.deltaTime * 2.5f);
    }

    void Des()
    {
        Destroy(gameObject);
    }
}
