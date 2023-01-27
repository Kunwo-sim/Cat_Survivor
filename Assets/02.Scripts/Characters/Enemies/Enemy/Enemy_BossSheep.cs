using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using Random = UnityEngine.Random;

public class Enemy_BossSheep : Enemy
{
    [SerializeField] private GameObject sweepReady;
    [SerializeField] private GameObject sweep;
    [SerializeField] private GameObject meteorReady;
    [SerializeField] private GameObject meteor;
    [SerializeField] private GameObject meteorExplosion;
    
    protected override void Routine()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                StartCoroutine(Routine_Dash());
                break;
            case 1:
                StartCoroutine(Routine_Sweep());
                break;
            case 2:
                StartCoroutine(Routine_Meteor());
                break;
        }
    }
    
    protected override IEnumerator Routine_Move()
    {
        state = CharacterState.Move;
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        Routine();
    }
    protected override IEnumerator Routine_Dash()
    {
        state = CharacterState.Attack;

        yield return new WaitForSeconds(1f);
        Vector3 dashDir = GetDirection(transform.position, _player.transform.position);
        _collider.isTrigger = true;
        float dashSpeed = 30;
        _rigidbody.AddForce(dashDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        _rigidbody.velocity = Vector2.zero;
        _collider.isTrigger = false;
        yield return new WaitForSeconds(2f);
        StartCoroutine(Routine_Move());
    }
    private IEnumerator Routine_Sweep()
    {
        state = CharacterState.Attack;
        var v = new Vector3(0, 0, 1);
        GameObject sl = Instantiate(sweepReady, transform.position + v, transform.rotation);
        yield return new WaitForSeconds(1.5f);
        Destroy(sl);
        GameObject s = Instantiate(sweep, transform.position + v, transform.rotation);
        s.GetComponent<EnemyAttack>().Damage = 7;
        yield return new WaitForSeconds(0.25f);
        Destroy(s);
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(Routine_Move());
    }
    private IEnumerator Routine_Meteor()
    {
        state = CharacterState.Attack;
        Vector3 pp = _player.transform.position;
        yield return new WaitForSeconds(0.5f);
        GameObject mr = Instantiate(meteorReady, pp, Quaternion.identity);
        Destroy(mr, 2.33f);
        GameObject m = Instantiate(meteor, pp + new Vector3(-xSpawnLimit, ySpawnLimit, -1), Quaternion.identity);
        GameObject me = Instantiate(meteorExplosion, pp, Quaternion.identity);
        me.GetComponent<EnemyAttack>().Damage = 5;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Routine_Move());
    }
    
    protected override IEnumerator  DeathFadeOut()
    {
        _collider.enabled = false;
        
        float max = 0.99f;
        for (float i = 0f; i <= max; i += 0.01f)
        {
            float value = max-i;
            _renderer.color = new Color(value, value, value);
            transform.Rotate(0,0,5);
            transform.localScale = new Vector3(value, value, value);
            yield return new WaitForSeconds(0.01f);
        }
        ObjectPoolManager.ReturnObject(gameObject, _poolType);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _player.ReceiveDamage(15);
        }
    }
}