using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class Bar : MonoBehaviour
    {
        private Slider _bar;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _bar = GetComponent<Slider>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetBar(float max, float now)
        {
            _bar.value = now/max;
        }
        public void SetText(int text)
        {
            _text.text = text.ToString();
        }
    }
}