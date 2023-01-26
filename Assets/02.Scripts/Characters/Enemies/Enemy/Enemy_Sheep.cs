using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Enemy_Sheep : Enemy
{
    private float _dir2;
    
    protected override void Move(Vector2 input)
    {
        if (state != CharacterState.Move) return;
        var dir = input.normalized;
        transform.Translate(dir * (MoveSpeed * 0.03f * _dir2));
    }
    protected override IEnumerator Routine_Move()
    {
        state = CharacterState.Move;

        for (float i = 0; i < 2; i += 0.25f)
        {
            _dir2 = Vector2.Distance(transform.position, _player.transform.position) > 8 ? 1 : -1.3f;
            yield return new WaitForSeconds(0.25f);
        }
        Routine();
    }

    protected override void FlipXRenderer(Vector2 direction)
    {
        _renderer.flipX = _dir2 < 0;
    }
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