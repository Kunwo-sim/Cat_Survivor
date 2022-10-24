using System;
using PlayLogic;
using UnityEngine;

namespace Characters
{
    public class Player : Character
    {
        private JoyStick _joyStick;
        private SkillHolder _skillHolder;

        protected override void Awake()
        {
            base.Awake();
            _joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
            _skillHolder = GetComponentInChildren<SkillHolder>();
            
            // Test code
            MoveSpeed = 5.0f;
        }

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
            
        }

        protected override void Death()
        {
            base.Death();
            
        }
    }
}