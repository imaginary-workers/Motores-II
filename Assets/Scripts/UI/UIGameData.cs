using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProyectM2.Persistence;

namespace ProyectM2
{
    public class UIGameData : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private TextMeshProUGUI _totalCurrencyGainText;
        ValuesToSaveInJson _myJsonData;
        private DataPersistance _myGameData;

        private void Awake()
        {
            _myGameData = GetComponent<DataPersistance>();
        }

        public void GetCurrencyData()
        {
            _myGameData.LoadGame();
            _currencyText.text += _myJsonData.totalCurrencyOfPlayer;
            _totalCurrencyGainText.text += _myJsonData.totalCurrencyGainOfPlayer;
        }
    }
}
