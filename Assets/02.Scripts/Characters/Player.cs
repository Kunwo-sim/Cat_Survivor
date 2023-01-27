using UnityEngine;

public class Player : Character
{
    private JoyStick _joyStick;
    private SkillHolder _skillHolder;
    private float _maxExp = 10;
    private float _exp = 0;
    private Bar _expBar;
    private Animator _animator;    
    public int HpRegen { get; set; } = 1;
    public int MeleeAttack { get; set; } = 0;
    public int RangeAttack { get; set; } = 0;
    public int Attack { get; set; } = 0;
    public int Critical { get; set; } = 0;

    public JoyStick PjoyStick
    {
        get { return _joyStick; }
    }

    private float _lastHpReGenTime = 0.0f;
    protected override void Awake()
    {
        base.Awake();
        _joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
        _skillHolder = GetComponentInChildren<SkillHolder>();
        _expBar = GameObject.Find("Exp Bar").GetComponent<Bar>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        // Test code
        _defaultMoveSpeed = 5.0f;
        MaxHp = 10;
        Hp = 10;
        Defense = 10;
        _skillHolder.skillList.Add(GetComponentInChildren<Skill_NyanCat>());
        _skillHolder.skillList.Add(GetComponentInChildren<Skill_NyanPunch>());
        _skillHolder.skillList.Add(GetComponentInChildren<Skill_Bong>());
        
        base.Start();
        SetHpUI();
        _expBar.SetBar(_maxExp, _exp);
        _expBar.SetText(Level);
    }
    private void Update()
    {
        if (HpRegen <= 0)
            return;

        _lastHpReGenTime += Time.deltaTime;
        float hpRecoveryTime = 5.0f / (1.0f + ((HpRegen - 1) / 2.25f));
        if (_lastHpReGenTime > hpRecoveryTime)
        {
            Hp += 1;
            _lastHpReGenTime = 0.0f;
        }    
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
        _skillHolder.transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    }

    private void LevelUp()
    {
        _exp -= _maxExp;
        _maxExp *= 1.2f;
        Level++;
        LevelCnt++;
        _expBar.SetText(Level);
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

    public override void ReceiveDamage(float damage, Vector3 knockBackDir = default, bool bCritical = false)
    {
        base.ReceiveDamage(damage);
        // SoundManager.Instance.PlaySFXSound("Player_Hit");
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