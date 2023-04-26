using ProyectM2.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2
{
    public class GasUi : MonoBehaviour
    {
        public float _maxTime;
        public float _currentTime;
        public Slider _slider;

        void Start()
        {
            _maxTime = GameManager.levelGas;
            _currentTime = _maxTime;
            _slider.maxValue = _maxTime;
            _slider.value = _currentTime;
        }

        void Update()
        {
            _currentTime -= Time.deltaTime;
            _slider.value = _currentTime;
        }

        public void ReloadSlider()
        {
            _currentTime = _maxTime;
            _slider.value = _currentTime;
        }

        private void OnEnable()
        {
            EventManager.StartListening("GasModified", OnGasModified);
        }

        private void OnGasModified(object[] obj)
        {
            if (obj.Length == 0) return;
            _currentTime = (float)obj[0];
        }
        private void OnDisable()
        {
            EventManager.StopListening("GasModified", OnGasModified);
        }
    }
}
