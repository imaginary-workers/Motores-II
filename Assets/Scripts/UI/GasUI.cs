using ProyectM2.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class GasUI : MonoBehaviour
    {
        [SerializeField] private float _maxTime;
        [SerializeField] private Slider _slider;
        private float _currentTime;

        void Start()
        {
            _maxTime = 100;
            _currentTime = MyGameManager.levelGas;
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
