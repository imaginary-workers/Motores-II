using ProyectM2.SO;
using TMPro;
using UnityEngine;

namespace ProyectM2.UI
{
    public class GameplayCurrencyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private DataIntObservable _playerLevelCurrency;

        private void OnEnable()
        {
            Debug.Log("GameplayCurrencyUI");
            OnCurrencyModified();
            _playerLevelCurrency.Subscribe(OnCurrencyModified);
        }

        private void OnDisable()
        {
            _playerLevelCurrency.Unsubscribe(OnCurrencyModified);
        }

        private void OnCurrencyModified()
        {
            _coinText.text = _playerLevelCurrency.value.ToString();
        }
    }
}