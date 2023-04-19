using UnityEngine;

namespace ProyectM2.Managers
{
    public class SceneManager : MonoBehaviour
    {
        public void ChangeScene(string nextScene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }
}