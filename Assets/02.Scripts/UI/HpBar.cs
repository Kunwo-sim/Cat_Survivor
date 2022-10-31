using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HpBar : MonoBehaviour
    {
        private Slider _hpBar;

        private void Awake()
        {
            _hpBar = GetComponent<Slider>();
        }

        public void SetHpBar(float maxHp, float hp)
        {
            _hpBar.value = hp/maxHp;
        }
    }
}
