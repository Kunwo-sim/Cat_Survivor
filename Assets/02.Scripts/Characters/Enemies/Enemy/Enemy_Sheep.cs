using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Enemy_Sheep : Enemy
{
    protected override void Routine()
    {
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                StartCoroutine(Routine_Move());
                break;
            case 1:
                StartCoroutine(Routine_Shot());
                break;
        }
    }
}
