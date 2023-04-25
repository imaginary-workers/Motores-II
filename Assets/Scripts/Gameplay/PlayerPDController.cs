using UnityEngine;
using UnityEngine.Playables;

namespace ProyectM2.Gameplay
{
    public class PlayerPDController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _playableDirector;
        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
        }

        private void OnEnemyCutSceneStarted(object[] obj)
        {
            _playableDirector.Play();
        }

        private void OnDisable()
        {
            EventManager.StopListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
        }
    }
}
