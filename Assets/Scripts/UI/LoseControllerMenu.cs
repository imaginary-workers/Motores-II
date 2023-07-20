using System;
using ProyectM2.Ads;
using ProyectM2.Stamina;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class LoseControllerMenu : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI staminaText = null;
        private void Awake()
        {
            _restartButton.interactable = StaminaSystem.Instance.HasEnoughStamina(1);
        }

        public void RewardStamina()
        {
            AdsManager.Instance.ShowRewardedAd();
        }
        private void OnEnable()
        {
            EventManager.StartListening("UpdateStamina", UpdateStaminaUI);
        }

        private void UpdateStaminaUI(object[] obj)
        {
            staminaText.text = StaminaSystem.Instance.CurrentStamina.ToString();
            _restartButton.interactable = StaminaSystem.Instance.HasEnoughStamina(1);
        }

        private void OnDisable()
        {
            EventManager.StopListening("UpdateStamina", UpdateStaminaUI);
        }
    }
}