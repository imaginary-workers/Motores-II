using ProyectM2.Scenes;
using UnityEngine;

namespace ProyectM2
{
    public class MenuGameplay : MonoBehaviour
    {
        public void MainMenu()
        {
            SceneManager.Instance.ChangeToMenuScene("Menu Principal");
        }
    }
}
