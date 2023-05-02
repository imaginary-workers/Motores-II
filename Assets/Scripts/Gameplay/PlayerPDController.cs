using UnityEngine;
using UnityEngine.Playables;

namespace ProyectM2.Gameplay
{
    public class PlayerPDController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private PlayableAsset _toEnemyView;
        [SerializeField] private PlayableAsset _toNormalView;
        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
            EventManager.StartListening("EnemyDiedCutSceneStarted", OnEnemyDiedCutSceneStarted);
        }

        private void OnDisable()
        {
            EventManager.StopListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
            EventManager.StopListening("EnemyDiedCutSceneStarted", OnEnemyDiedCutSceneStarted);
        }

        private void OnEnemyDiedCutSceneStarted(object[] obj)
        {
            //TODO timeline de muerte del died
            _playableDirector.playableAsset = _toNormalView;
            _playableDirector.Play();
        }

        private void OnEnemyCutSceneStarted(object[] obj)
        {
            _playableDirector.playableAsset = _toEnemyView;
            _playableDirector.Play();
        }
    }
}
