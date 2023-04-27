using ProyectM2.Gameplay;
using ProyectM2.Managers;
using UnityEngine;

namespace ProyectM2
{
    public class MenuGameplay : MonoBehaviour
    {
        public void MainMenu()
        {
            SceneManager.Instance.ChangeToMenuScene("Menu Principal");
        }

        // public void RestartLevel()
        // {
        //     GameManager.Retry();
        //     SceneManager.Instance.RestartLevel();
        // }
    }
}
