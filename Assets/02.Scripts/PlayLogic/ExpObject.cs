using UnityEngine;
using DG.Tweening;
public class ExpObject : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Player _player;
    private float _exp;
    private readonly EPoolObjectType _poolType = EPoolObjectType.ExpObject;
    private bool _bIsAcquire = false;
    private float _moveSpeed = 0.0f;

    private void Awake()
    {
        _bIsAcquire = false;
        _moveSpeed = 0.0f;
        _renderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void Initialize(float exp, Vector3 position)
    {
        _exp = exp;

        if (_exp < 2)
        {
            _renderer.color = Color.white;
        }
        else if (_exp < 5)
        {
            _renderer.color = Color.green;
        }
        else
        {
            _renderer.color = Color.yellow;
        }

        transform.position = position;
    }

    void Update()
    {
        if (_bIsAcquire)
        {
            Vector3 dir = new Vector3(_player.transform.position.x - transform.position.x, _player.transform.position.y - transform.position.y, 0);
            dir = dir.normalized;
            transform.position += (dir * _moveSpeed);
            _moveSpeed += 0.01f;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _bIsAcquire = false;
            ObjectPoolManager.ReturnObject(gameObject, _poolType);
            _player.ReceiveExp(_exp);

            //Sequence sequence = DOTween.Sequence();
            //Vector2 curPos = transform.position;
            //Vector2 moveOffset = _player.PjoyStick.JoyDirection * 10;
            //Vector3 playerPos = _player.transform.position;

            //sequence.Append(transform.DOMove(curPos + moveOffset, 0.5f).SetEase(Ease.OutQuad))
            //    .Append(transform.DOMove(playerPos, 0.5f).SetEase(Ease.InQuad))
            //    .OnComplete(ExpCallback);
            //sequence.Play();
        }
        else if (col.CompareTag("PlayerMagnet"))
        {
            _bIsAcquire = true;
        }
    }
}