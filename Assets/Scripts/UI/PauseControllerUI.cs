using System.Collections;
using ProyectM2.Gameplay;
using TMPro;
using UnityEngine;

namespace ProyectM2.UI
{
    public class PauseControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private TextMeshProUGUI _pauseCounterText;

        public void SetPauseMenu(bool pause)
        {
            _pauseButton.SetActive(!pause);
            _pauseMenu.SetActive(pause);
            if (pause)
            {
                ScreenManager.Instance.Pause();
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
        }
    }
}
