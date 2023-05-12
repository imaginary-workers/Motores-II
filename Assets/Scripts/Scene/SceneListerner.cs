using ProyectM2.Managers;
using UnityEngine;

namespace ProyectM2.Scenes
{
    public class SceneListerner : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.StartListening("ChangeScene", ChangeSceneHandler);
        }

        private void ChangeSceneHandler(object[] obj)
        {
            if (obj.Length == 0) return;
            SceneManager.Instance.ChangeScene((Scene)obj[0]);
        }

        private void OnDisable()
        {
            EventManager.StopListening("ChangeScene", ChangeSceneHandler);

        }
    }
}