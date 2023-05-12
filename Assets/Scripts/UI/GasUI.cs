using ProyectM2.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2
{
    public class GasUI : MonoBehaviour
    {
        public float _maxTime;
        public float _currentTime;
        public Slider _slider;

        void Start()
        {
            _maxTime = 100;
            _currentTime = GameManager.levelGas;
            _slider.maxValue = _maxTime;
            _slider.value = _currentTime;
        }

        private void OnEnable()
        {
            EventManager.StartListening("GasModified", OnGasModified);
            EventManager.StartListening("GasSubtract", OnGasModified);
        }

        private void OnGasModified(object[] obj)
        {
            if (obj.Length == 0) return;
            _currentTime = (float)obj[0];
            _slider.value = _currentTime;
        }
        private void OnDisable()
        {
            EventManager.StopListening("GasModified", OnGasModified);
            EventManager.StopListening("GasSubtract", OnGasModified);
        }
    }
}
