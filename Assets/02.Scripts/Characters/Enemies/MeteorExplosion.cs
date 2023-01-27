using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorExplosion : EnemyAttack
{
    private Collider2D _col;
    private SpriteRenderer _ren;

    private void Awake()
    {
        _ren = GetComponent<SpriteRenderer>();        
        _col = GetComponent<Collider2D>();
    }
    void Start()
    {
        _col.enabled = false;
        _ren.enabled = false;
        Invoke(nameof(Shoot), 2.3f);
        Invoke(nameof(Des), 2.5f);
    }

    void Shoot()
    {
        _col.enabled = true;
        _ren.enabled = true;
        for (int i = 0; i < 360; i+=30)
        {
            EnemyProjectile ep = ObjectPoolManager.GetObject(EPoolObjectType.EnemyProjectile).GetComponent<EnemyProjectile>();
            ep.Initialize(transform.position, Quaternion.Euler(0,0,i), (int)3, 3, EPoolObjectType.EnemyProjectile);
        }
    }

    void Des()
    {
        Destroy(gameObject);
    }
}
