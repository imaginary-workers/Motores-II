using ProyectM2.Managers;
using UnityEngine;

namespace ProyectM2
{
    public class PauseControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _pauseMenu;

        private void OnEnable()
        {
            EventManager.TriggerEvent("OnPause");
            EventManager.TriggerEvent("OnResume");
        }

        private void OnDisable()
        {
        }

        private void OnResumeHandler()
        {
            SetPauseMenu(false);
        }

        public void SetPauseMenu(bool pause)
        {
            _pauseButton.SetActive(!pause);
            _pauseMenu.SetActive(pause);
            if (pause)
            {
                
            }
            else
            {
                
            }
        }

        private void OnPauseHandler()
        {
            SetPauseMenu(true);
        }
    }
}
