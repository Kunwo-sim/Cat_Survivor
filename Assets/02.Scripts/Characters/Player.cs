using System;
using PlayLogic;
using UnityEngine;

namespace Characters
{
    public class Player : Character
    {
        private JoyStick _joyStick;
        private SkillHolder _skillHolder;
        private float _maxExp = 10;
        private float _exp = 0;
        
        
        private void Update()
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
        }
        
        protected override void Awake()
        {
            base.Awake();
            _joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
            _skillHolder = GetComponentInChildren<SkillHolder>();
            
            // Test code
            MoveSpeed = 5.0f;
        }
        
        protected override void Death()
        {
            base.Death();
        }
        
        public void ReceiveExp(float exp)
        {
            _exp += exp;
            if (_exp >= _maxExp)
            {
                LevelUp();
                ReceiveExp(0f);
            }
        }
    }
}