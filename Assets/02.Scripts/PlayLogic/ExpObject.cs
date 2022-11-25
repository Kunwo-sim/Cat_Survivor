using UnityEngine;
using DG.Tweening;
public class ExpObject : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Player _player;
    private float _exp;
    private readonly EPoolObjectType _poolType = EPoolObjectType.ExpObject;
    private Tweener _backToPlayer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void Initialize(float exp, Vector3 position)
    {
        _exp = exp;
        float size = _exp * 0.02f + 0.3f;
        transform.localScale = new Vector3(size, size, 1);
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Sequence sequence = DOTween.Sequence();
            Vector2 curPos = transform.position;
            Vector2 moveOffset = _player.PjoyStick.JoyDirection * 10;
            Vector3 playerPos = _player.transform.position;

            sequence.Append(transform.DOMove(curPos + moveOffset, 0.5f).SetEase(Ease.OutQuad))
                .Append(transform.DOMove(playerPos, 0.5f).SetEase(Ease.InQuad))
                .OnComplete(ExpCallback);
            sequence.Play();
        }
    }

    public void ExpCallback()
    {
        ObjectPoolManager.ReturnObject(gameObject, _poolType);
        _player.ReceiveExp(_exp);
    }
}