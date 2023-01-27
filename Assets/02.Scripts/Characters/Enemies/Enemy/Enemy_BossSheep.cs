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

    private Bar _hpBar;

    protected override void Awake()
    {
        base.Awake();
        _hpBar = GetComponentInChildren<Bar>();
    }

    private void Update()
    {
        if(transform.position.x is < -xSpawnLimit or > xSpawnLimit) _rigidbody.velocity = Vector2.zero;
        if(transform.position.y is < -ySpawnLimit or > ySpawnLimit) _rigidbody.velocity = Vector2.zero;
    }

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

    protected override void Start()
    {
        base.Start();
        SoundManager.Instance.PlayBGMSound();
    }

    public override void ReceiveDamage(float damage, Vector3 knockBackDir = default, bool bCiritical = false)
    {
        DamagePopup d = Instantiate(_damagePopup);
        d.Setup((int)damage, transform.position, bCiritical);
        SoundManager.Instance.PlaySFXSound("SkillHit");
        base.ReceiveDamage(damage, Vector3.zero);
        _hpBar.SetBar(_maxHp, _hp);
        _hpBar.SetText($"{Hp} / {MaxHp}");
    }
    
    protected override IEnumerator Routine_Move()
    {
        _animator.SetTrigger("Idle");
        state = CharacterState.Idle;
        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        state = CharacterState.Move;
        _animator.SetTrigger("Run");
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        Routine();
    }
    protected override IEnumerator Routine_Dash()
    {
        state = CharacterState.Attack;
        _animator.SetTrigger("Dash");
        SoundManager.Instance.PlaySFXSound("Boss_Dash_Full");
        yield return new WaitForSeconds(1f);
        Vector3 dashDir = GetDirection(transform.position, _player.transform.position);
        _collider.isTrigger = true;
        float dashSpeed = 30;
        _rigidbody.AddForce(dashDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        _rigidbody.velocity = Vector2.zero;
        _collider.isTrigger = false;
        _animator.SetTrigger("Idle");
        yield return new WaitForSeconds(2f);
        StartCoroutine(Routine_Move());
    }
    private IEnumerator Routine_Sweep()
    {
        state = CharacterState.Attack;
        _animator.SetTrigger("Sweep");
        var v = new Vector3(0, 0, 1);
        GameObject sl = Instantiate(sweepReady, transform.position + v, transform.rotation);
        yield return new WaitForSeconds(1.5f);
        Destroy(sl);
        GameObject s = Instantiate(sweep, transform.position + v, transform.rotation);
        SoundManager.Instance.PlaySFXSound("Boss_Magic_Explosion");
        s.GetComponent<EnemyAttack>().Damage = 7;
        yield return new WaitForSeconds(0.25f);
        Destroy(s);
        yield return new WaitForSeconds(0.25f);
        _animator.SetTrigger("Idle");
        StartCoroutine(Routine_Move());
    }
    private IEnumerator Routine_Meteor()
    {
        state = CharacterState.Attack;
        _animator.SetTrigger("Meteor");
        SoundManager.Instance.PlaySFXSound("Boss_Meteor_Fall");
        SoundManager.Instance.PlaySFXSound("Boss_Meteor_Explosion");
        Vector3 pp = _player.transform.position;
        yield return new WaitForSeconds(0.5f);
        GameObject mr = Instantiate(meteorReady, pp, Quaternion.identity);
        Destroy(mr, 2.33f);
        GameObject m = Instantiate(meteor, pp + new Vector3(-xSpawnLimit-1, ySpawnLimit+1, -5), Quaternion.Euler(0, 0, -45));
        GameObject me = Instantiate(meteorExplosion, pp, Quaternion.identity);
        me.GetComponent<EnemyAttack>().Damage = 5;
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Idle");
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
        GameObject.Find("Canvas").transform.Find("GameClearPanel").gameObject.SetActive(true);
        Time.timeScale = 0;
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