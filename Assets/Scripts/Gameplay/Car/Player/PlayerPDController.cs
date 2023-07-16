using UnityEngine;
using UnityEngine.Playables;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerPDController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private PlayableAsset _toEnemyView;
        [SerializeField] private PlayableAsset _toNormalView;
        private void OnEnable()
        {
            CutSceneManager.Instance.Subscribe("EnemyArrival", CutSceneState.Started, OnEnemyCutSceneStarted);
            CutSceneManager.Instance.Subscribe("EnemyDied", CutSceneState.Started, OnEnemyDiedCutSceneStarted);
        }

        private void OnDisable()
        {
            CutSceneManager.Instance.Unsubscribe("EnemyArrival", CutSceneState.Started, OnEnemyCutSceneStarted);
            CutSceneManager.Instance.Unsubscribe("EnemyDied", CutSceneState.Started, OnEnemyDiedCutSceneStarted);
        }

        private void OnEnemyDiedCutSceneStarted()
        {
            _playableDirector.playableAsset = _toNormalView;
            _playableDirector.Play();
        }

        private void OnEnemyCutSceneStarted()
        {
            _playableDirector.playableAsset = _toEnemyView;
            _playableDirector.Play();
        }
    }
}
