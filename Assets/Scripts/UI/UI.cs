using TMPro;
using UnityEngine;

namespace ProyectM2.UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private int _coin;
        [SerializeField] private TextMeshProUGUI _coinText;
        // public TextMesh _coinText;

        private void Awake()
        {
            _coinText.text = _coin.ToString();
        }

        private void OnEnable()
        {
            EventManager.StartListening("CurrencyModified", OnCurrencyModified);
        }

        private void OnCurrencyModified(object[] obj)
        {
            if (obj.Length == 0) return;
            _coin = (int)obj[0];
            _coin = (int)obj[1];
            _coinText.text = _coin.ToString();
        }

        private void OnDisable()
        {
            EventManager.StopListening("CurrencyModified", OnCurrencyModified);
        }
    }
}