using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Enemy_1 : Enemy
{
    private Targeting _targeting = new Targeting();
    private EnemyProjectile cloneProjectile;

    
    protected override void Start()
    {
        base.Start();
        Routine();
    }
    void Routine()
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
    
    private IEnumerator Routine_Move()
    {
        state = CharacterState.Move;
        yield return new WaitForSeconds(1f);
        Routine();
    }
    private IEnumerator Routine_Shot()
    {
        state = CharacterState.Attack;
        yield return new WaitForSeconds(0.5f);
        var dir = GetDirection(transform.position, _player.transform.position);
        var pos = transform.position;
        var rot = _targeting.GetToNearRotate(dir);
        var poolType = EPoolObjectType.EnemyProjectile;
        
        cloneProjectile = ObjectPoolManager.GetObject(poolType).GetComponent<EnemyProjectile>();
        cloneProjectile.Initialize(pos, rot, (int)Power, 3, poolType);
        // SoundManager.Instance.PlaySFXSound("SkillShot");
        
        yield return new WaitForSeconds(0.25f);
        state = CharacterState.Move;
        yield return new WaitForSeconds(2f);
        Routine();
    }
}
