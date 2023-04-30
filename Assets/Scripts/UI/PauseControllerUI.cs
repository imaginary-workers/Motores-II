using UnityEngine;

namespace ProyectM2.UI
{
    public class PauseControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _pauseMenu;

        public void SetPauseMenu(bool pause)
        {
            _pauseButton.SetActive(!pause);
            _pauseMenu.SetActive(pause);
            if (pause)
            {
                EventManager.TriggerEvent("OnPause", true);
            }
            else
            {
                EventManager.TriggerEvent("OnPause", false);
            }
        }
    }
}
