using ProyectM2.Managers;
using UnityEngine;

namespace ProyectM2.UI
{
    public class MenuPrincipalUI : MonoBehaviour
    {
        public void Play(int level)
        {
            SceneManager.Instance.ChangeScene(new Scene("Level "+level, Scene.Type.Gameplay));
        }
    }
}
