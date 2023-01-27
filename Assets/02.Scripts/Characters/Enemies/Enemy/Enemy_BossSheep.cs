using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using Random = UnityEngine.Random;

public class Enemy_BossSheep : Enemy
{
    
    protected override void Routine()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                StartCoroutine(Routine_Dash());
                break;
            case 1:
                StartCoroutine(Routine_Dash());
                break;
            case 2:
                StartCoroutine(Routine_Dash());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.ReceiveDamage(15);
        }
    }
    protected override void OnTriggerStay(Collider other)
    {
    }
    
}