using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProyectM2
{
    public class StaminaUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI staminaText = null;
        [SerializeField] private TextMeshProUGUI staminaTimeText = null;

        private int _currentStamina;
        private int _maxStamina;

        public void UpdateTimer(DateTime nextStaminaTime)
        {
            if (_currentStamina >= _maxStamina)
            {
                staminaTimeText.text = "";
                return;
            }

            var timer = nextStaminaTime - DateTime.Now;
            var timeToShow = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
            staminaTimeText.text = timeToShow;
        }

        public void UpdateStamina(int currentStamina, int maxStamina)
        {
            _currentStamina = currentStamina;
            _maxStamina = maxStamina;
            staminaText.text = _currentStamina.ToString() + "/" + _maxStamina.ToString();
        }
    }
}
