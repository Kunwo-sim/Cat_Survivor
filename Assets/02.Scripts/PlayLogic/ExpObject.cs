using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ExpObject : MonoBehaviour
{
    [SerializeField]
    private RectTransform _moneyUI;
    private SpriteRenderer _renderer;
    private Player _player;
    private float _exp;
    private int _money;
    private readonly EPoolObjectType _poolType = EPoolObjectType.ExpObject;
    private bool _bIsAcquire = false;
    public bool _bToUI = false;
    private float _moveSpeed = 0.0f;

    Vector3 UIPosition = Vector3.zero;

    private void Awake()
    {
        _moneyUI = GameObject.Find("MoneyUI").GetComponent<RectTransform>();
        _bIsAcquire = false;
        _moveSpeed = 0.0f;
        _renderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void Initialize(int money, float exp, Vector3 position)
    {
        _money = money;
        _exp = exp;
        _bToUI = false;

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
        Vector3 dir = Vector3.zero;
        if (_bToUI)
        {
            UIPosition = Camera.main.ScreenToWorldPoint(_moneyUI.transform.position);
            dir = UIPosition - transform.position;

            if (dir.magnitude < 1f)
            {
                _bToUI = false;
                InGameManager.Instance.Money += _money;
                ObjectPoolManager.ReturnObject(gameObject, _poolType);
            }

            dir = dir.normalized;
            transform.position += (dir * _moveSpeed);
            _moveSpeed += 0.01f;
        }
        else if (_bIsAcquire)
        {
            dir = _player.transform.position - transform.position;
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
            InGameManager.Instance.Money += _money;
        }
        else if (col.CompareTag("PlayerMagnet"))
        {
            _bIsAcquire = true;
        }
    }
}