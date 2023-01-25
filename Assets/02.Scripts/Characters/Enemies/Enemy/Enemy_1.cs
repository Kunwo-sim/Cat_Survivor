using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Enemy_1 : Enemy
{
    protected override void Routine()
    {
        int random = Random.Range(0, 1);
        switch (random)
        {
            case 0:
                StartCoroutine(Routine_Move());
                break;
        }
    }
}
