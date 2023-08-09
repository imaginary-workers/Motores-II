using ProyectM2.Ads;
using ProyectM2.Gameplay;
using ProyectM2.Stamina;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class LoseControllerMenu : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private GameObject _warning;
        [SerializeField] private TextMeshProUGUI _warningText;
        [SerializeField] private MyGameManager _myGameManager;
        [SerializeField] private string _warningAddTextToShow;
        [SerializeField] private string _warningRestartLevelTextToShow;
        [SerializeField] private string _warningGoToMainMenuTextToShow;

        private void Awake()
        {
            ActiveWargingUI(false);
        }

        private void RewardStamina()
        {
            AdsManager.Instance.ShowRewardedAd();
        }

        private void UpdateStaminaUI(object[] obj)
        {
            ActiveWargingUI(false);
            _restartButton.interactable = StaminaSystem.Instance.HasEnoughStamina(1);
        }

        public void ActiveWargingUI(bool active)
        {
            _warning.SetActive(active);
        }

        public void WarningAdd()
        {
            ActiveWargingUI(true);
            _warningText.text = _warningAddTextToShow;
            _yesButton.onClick.RemoveAllListeners();
            _yesButton.onClick.AddListener(RewardStamina);
        }

        public void WarningRetryLevel()
        {
            ActiveWargingUI(true);
            _warningText.text = _warningRestartLevelTextToShow;
            _yesButton.onClick.RemoveAllListeners();
            _yesButton.onClick.AddListener(_myGameManager.Retry);
        }

        public void WarningMainMenuLevel()
        {
            ActiveWargingUI(true);
            _warningText.text = _warningGoToMainMenuTextToShow;
            _yesButton.onClick.RemoveAllListeners();
            _yesButton.onClick.AddListener(_myGameManager.QuitGame);
        }

        private void OnEnable()
        {
            EventManager.StartListening("LoseCanvasActive", UpdateStaminaUI);
            EventManager.StartListening("RechargeStamina", UpdateStaminaUI);
        }

        private void OnDisable()
        {
            EventManager.StopListening("LoseCanvasActive", UpdateStaminaUI);
            EventManager.StopListening("RechargeStamina", UpdateStaminaUI);
        }

    }
}