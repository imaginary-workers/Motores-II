using System;
using ProyectM2.Ads;
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

        private void Start()
        {
            AdsManager.Instance.LoadRewardedAd();
        }

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

        public void StaminaAd()
        {
            AdsManager.Instance.ShowRewardedAd();
        }
    }
}
