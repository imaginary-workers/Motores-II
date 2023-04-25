using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2
{
    public class GasUi : MonoBehaviour
    {
        public float _maxTime = 10f;
        public float _currentTime;
        public Slider _slider;

        void Start()
        {
            _currentTime = _maxTime;
            _slider.maxValue = _maxTime;
            _slider.value = _currentTime;
        }

        void Update()
        {
            _currentTime -= Time.deltaTime;
            _slider.value = _currentTime;

            if (_currentTime <= 0)
            {
                //Debug.Log("Perdisdte");
            }
        }

        public void ReloadSlider()
        {
            _currentTime = _maxTime;
            _slider.value = _currentTime;
        }
    }
}
