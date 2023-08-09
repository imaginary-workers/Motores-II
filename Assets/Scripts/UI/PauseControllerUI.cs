using System;
using System.Collections;
using ProyectM2.Gameplay;
using ProyectM2.Stamina;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class PauseControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private Button _retryButton;
        [SerializeField] private TextMeshProUGUI _pauseCounterText;
        public static bool isPause;

        private void Awake()
        {
            _pauseButton.SetActive(false);
        }

        public void SetPauseMenu(bool pause)
        {
            isPause = pause;
            _pauseMenu.SetActive(pause);
            if (pause)
            {
                _pauseButton.SetActive(!pause);
                ScreenManager.Instance.Pause();
                _retryButton.interactable = StaminaSystem.Instance.HasEnoughStamina(1);
                StopAllCoroutines();
            }
            else
            {
                StartCountingDownToStart();
            }
        }

        public void StartCountingDownToStart()
        {
            StartCoroutine(CO_ResumeGame());
        }

        private IEnumerator CO_ResumeGame()
        {
            yield return new WaitForSecondsRealtime(.5f);
            _pauseCounterText.gameObject.SetActive(true);
            for (int i = 3; i > 0; i--)
            {
                _pauseCounterText.text = i.ToString();
                yield return new WaitForSecondsRealtime(1);
            }

            _pauseCounterText.gameObject.SetActive(false);
            ScreenManager.Instance.Resume();
            _pauseButton.SetActive(true);
        }
    }
}