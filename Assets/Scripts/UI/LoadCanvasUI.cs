using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class LoadCanvasUI : MonoBehaviour
    {
        [SerializeField] private GameObject _loadScreen;
        [SerializeField] private TextMeshProUGUI _loadText;
        [SerializeField] private Slider _loadBar;
        [SerializeField] private AdviceSO _adviceSO;
        [SerializeField] TextMeshProUGUI _adviceText;
        [SerializeField] Button _startButton;
        Coroutine _adviceCoroutine;
        public void DisplayLoadCanvas(bool display)
        {
            _loadScreen.SetActive(display);
            if (display)
            {
                if (_adviceText != null && _adviceSO.advice.Count > 0)
                {
                    _adviceCoroutine = StartCoroutine(ChangeAdviceCoroutine());
                }
            }
        }

        public void SetLoadTextTo(string loadText)
        {
            _loadText.text = loadText;
        }

        public void SetLoadBarTo(float loadValue)
        {
            _loadBar.value = loadValue;
        }
        public void VisualButton()
        {
            _startButton.gameObject.SetActive(true);
        }
        public void OnPressButton()
        {
            DisplayLoadCanvas(false);
            EventManager.TriggerEvent("SceneLoadComplete");
            _startButton.gameObject.SetActive(false);
        }
        IEnumerator ChangeAdviceCoroutine()
        {
            while (true)
            {
                int randomIndex = Random.Range(0, _adviceSO.advice.Count);
                _adviceText.text = _adviceSO.advice[randomIndex];
                yield return new WaitForSeconds(3f);
            }
        }
    }
}