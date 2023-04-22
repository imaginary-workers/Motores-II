using ProyectM2.Managers;
using UnityEngine;

namespace ProyectM2
{
    public class MenuPrincipalUI : MonoBehaviour
    {
        public void Play(string levelScene)
        {
            SceneManager.Instance.ChangeScene(levelScene);
        }
    }
}
