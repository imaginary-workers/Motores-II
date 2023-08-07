using System;
using ProyectM2.Ads;
using TMPro;
using UnityEngine;

namespace ProyectM2.Stamina
{
    public class StaminaUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI staminaText = null;
        [SerializeField] private TextMeshProUGUI staminaTimeText = null;

        private int _currentStamina;
        private int _maxStamina;

        private void Start()
        {
            AdsManager.Instance.LoadRewardedAd();
        }

        private void OnEnable()
        {
            Debug.Log($"On Enable desde StaminaUI. Mi GO es {gameObject.name}");
            EventManager.StartListening("UpdateStamina", UpdateStamina);
            EventManager.StartListening("ModifyStaminaTimer", UpdateTimer);
        }

        private void OnDisable()
        {
            EventManager.StopListening("UpdateStamina", UpdateStamina);
            EventManager.StopListening("ModifyStaminaTimer", UpdateTimer);
        }

        private void UpdateTimer(object[] obj)
        {
            if (_currentStamina >= _maxStamina)
            {
                staminaTimeText.text = "";
                return;
            }

            var timer = (DateTime)obj[0] - DateTime.Now;
            var timeToShow = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
            staminaTimeText.text = timeToShow;
        }

        private void UpdateStamina(object[] obj)
        {
            _currentStamina = (int)obj[0];
            _maxStamina = (int)obj[1];
            staminaText.text = _currentStamina.ToString() + "/" + _maxStamina.ToString();
        }

        public void StaminaAd()
        {
            AdsManager.Instance.ShowRewardedAd();
        }
    }
}
