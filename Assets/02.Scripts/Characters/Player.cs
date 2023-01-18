using UnityEngine;

public class Player : Character
{
    private JoyStick _joyStick;
    private SkillCoolManager _skillCoolManager;
    private float _maxExp = 10;
    private float _exp = 0;
    private Bar _expBar;
    private Animator _animator;

    public JoyStick PjoyStick
    {
        get { return _joyStick; }
    }

    protected override void Awake()
    {
        base.Awake();
        _joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
        _skillCoolManager = GetComponentInChildren<SkillCoolManager>();
        _expBar = GameObject.Find("Exp Bar").GetComponent<Bar>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        // Test code
        MoveSpeed = 3.0f;
        MaxHp = 100;
        Hp = 100; 
        _exp = 100;
        LevelUp();
        
        base.Start();
        SetHpUI();
        _expBar.SetBar(_maxExp, _exp);
        _expBar.SetText(_level);
    }
    private void FixedUpdate()
    {
        Move(_joyStick.JoyDirection);
        TurnSkillHolder();
        SetAnimation();
    }

    private void SetAnimation()
    {
        if (_joyStick.JoyDirection == Vector2.zero)
        {
            _animator.SetBool("isMove", false);
        }
        else
        {
            _animator.SetBool("isMove", true);
            if (_joyStick.JoyDirection.x < 0)
            {
                _renderer.flipX = true;
            }
            else
            {
                _renderer.flipX = false;
            }
        }
    }
    
    private void TurnSkillHolder()
    {
        float rot = Mathf.Atan2(_joyStick.LastJoyDirection.y, _joyStick.LastJoyDirection.x) * Mathf.Rad2Deg;
        _skillCoolManager.transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    }

    private void LevelUp()
    {
        _exp -= _maxExp;
        _maxExp *= 1.2f;
        _level++;
        _expBar.SetText(_level);
    }

    public void ReceiveExp(float exp)
    {
        _exp += exp;
        if (_exp >= _maxExp)
        {
            LevelUp();
        }
        _expBar.SetBar(_maxExp, _exp);
    }

    public override void ReceiveDamage(float damage, Vector3 knockBackDir = default)
    {
        base.ReceiveDamage(damage);
        SetHpUI();
    }

    protected override void Death()
    {
        base.Death();
        // Test
        GameObject.Find("Canvas").transform.Find("GameOverPanel").gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}