using ProyectM2.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private int _coin;
        [SerializeField] private int _coinPlus;
        public TextMesh _coinText;

        void Update()
        {
            _coin = GameManager.levelCurrency;
            _coinText.text = "Score: " + _coin.ToString();
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
        }

        private void OnDisable()
        {
            EventManager.StopListening("CurrencyModified", OnCurrencyModified);
        }
    }
}