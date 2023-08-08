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

        public void RewardStamina()
        {
            AdsManager.Instance.ShowRewardedAd();
        }
        private void OnEnable()
        {
            EventManager.StartListening("LoseCanvasActive", UpdateStaminaUI);
        }

        private void UpdateStaminaUI(object[] obj)
        {
            _restartButton.interactable = StaminaSystem.Instance.HasEnoughStamina(1);
        }

        private void OnDisable()
        {
            EventManager.StopListening("LoseCanvasActive", UpdateStaminaUI);
        }
    }
}