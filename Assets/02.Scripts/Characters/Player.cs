using System;
using PlayLogic;
using UnityEngine;

namespace Characters
{
    public class Player : Character
    {
        private JoyStick _joyStick;

        protected override void Awake()
        {
            base.Awake();
            _joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
        }

        private void Update()
        {
            Move(_joyStick.JoyDirection);
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