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
        [SerializeField] private TextMeshProUGUI _adviceText;
        [SerializeField] private Button _startButton;
        [SerializeField] private float _timeBetweenAdvices = 3f;
        private bool _coroutineStart = false;

        public void DisplayLoadCanvas(bool display)
        {
            _loadScreen.SetActive(display);
            if (display)
            {
                if (_adviceText != null && _adviceSO.advice.Count > 0 && !_coroutineStart)
                {
                    StartCoroutine(ChangeAdviceCoroutine());
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

        int GenerateRandomIndex(int previousIndex)
        {
            int randomIndex = Random.Range(0, _adviceSO.advice.Count);

            while (randomIndex == previousIndex)
            {
                randomIndex = Random.Range(0, _adviceSO.advice.Count);
            }

            return randomIndex;
        }

        IEnumerator ChangeAdviceCoroutine()
        {
            _coroutineStart = true;
            var changeInterval = _timeBetweenAdvices;
            var elapsedTime = 0f;
            int previousIndex = -1;

            while (true)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= changeInterval)
                {
                    int randomIndex = GenerateRandomIndex(previousIndex);
                    _adviceText.text = _adviceSO.advice[randomIndex];
                    elapsedTime = 0f;
                }

                yield return null;
            }
        }
    }
}