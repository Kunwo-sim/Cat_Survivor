using UnityEngine;

public class Player : Character
{
    private JoyStick _joyStick;
    private SkillHolder _skillHolder;
    private float _maxExp = 10;
    private float _exp = 0;
    private Bar _expBar;
    protected override void Awake()
    {
        base.Awake();
        _joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
        _skillHolder = GetComponentInChildren<SkillHolder>();
        _expBar = GameObject.Find("Exp Bar").GetComponent<Bar>();

        // Test code
        MoveSpeed = 3.0f;
        MaxHp = 100;
    }
    protected override void Start()
    {
        base.Start();
        SetHpUI();
        _expBar.SetBar(_maxExp, _exp);
        _expBar.SetText(Level);
    }
    private void FixedUpdate()
    {
        Move(_joyStick.JoyDirection);
        TurnSkillHolder();
    }

    private void TurnSkillHolder()
    {
        float rot = Mathf.Atan2(_joyStick.LastJoyDirection.y, _joyStick.LastJoyDirection.x) * Mathf.Rad2Deg;
        _skillHolder.transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    }

    private void LevelUp()
    {
        _exp -= _maxExp;
        _maxExp *= 1.5f;
        Level++;
        _expBar.SetText(Level);
    }

    public void ReceiveExp(float exp)
    {
        _exp += exp;
        _expBar.SetBar(_maxExp, _exp);
        if (_exp >= _maxExp)
        {
            LevelUp();
        }
    }

    public override void ReceiveDamage(float damage)
    {
        SetHpUI();
        base.ReceiveDamage(damage);
    }

    protected override void Death()
    {
        base.Death();
        // Test
        Debug.Log("Game Over !");
        Time.timeScale = 0;
    }
}