using System;
using ProyectM2.Ads;
using ProyectM2.Stamina;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class LoseControllerMenu: MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        private void Awake()
        {
            _restartButton.interactable = StaminaSystem.Instance.HasEnoughStamina(1);
        }

        public void RewardStamina()
        {
            AdsManager.Instance.ShowRewardedAd();
        }
    }
}