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
#if UNITY_EDITOR
        [SerializeField] private string debuScene;
        [ContextMenu("Play Debug")]
        public void Play()
        {
            SceneManager.Instance.ChangeScene(new Scene(debuScene, Scene.Type.Gameplay));
        }
#endif
    }
}
