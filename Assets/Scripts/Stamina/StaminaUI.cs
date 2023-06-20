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
        [SerializeField] TextMeshProUGUI maxStaminaText = null;
        [SerializeField] private TextMeshProUGUI staminaTimeText = null;


        private void OnEnable()
        {
            EventManager.StartListening("UpdateStaminaTimerUI", UpdateTimer);    
            EventManager.StartListening("UpdateStaminaUI", UpdateStamina);
        }

        private void OnDisable()
        {
            EventManager.StopListening("UpdateStaminaTimerUI", UpdateTimer);
            EventManager.StopListening("UpdateStaminaUI", UpdateStamina);

        }

        public void UpdateTimer(object[] obj)
        {
            if (obj.Length == 0) return;
            if (!staminaTimeText)
                staminaTimeText.text = (string)obj[0];
        }

        private void UpdateStamina(object[] obj)
        {
            if (obj.Length == 0) return;
            if (!staminaText)
                staminaText.text = (string)obj[0];
        }
    }
}
