using System.Collections.Generic;
using ProyectM2.Ads;
using ProyectM2.Stamina;
using ProyectM2.UI.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class LoseControllerMenu : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private GameObject _warningAdd;
        [SerializeField] private GameObject _warningRestarLevel;
        [SerializeField] private GameObject _warningGoToMainMenu;
        private Stack<ICommand> commandStack = new Stack<ICommand>();


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