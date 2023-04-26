using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ProyectM2
{
    public class UIGameData : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private TextMeshProUGUI _totalCurrencyGainText;
        ValuesToSaveInJson _myJsonData;

        public void GetCurrencyData()
        {
            _currencyText.text += _myJsonData.totalCurrencyOfPlayer;
            _totalCurrencyGainText.text += _myJsonData.totalCurrencyGainOfPlayer;

        }
    }
}
