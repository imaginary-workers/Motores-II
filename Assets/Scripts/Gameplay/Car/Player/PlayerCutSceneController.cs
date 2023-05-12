using System.Collections;
using ProyectM2.Car.Controller;
using ProyectM2.Gameplay.Car.Controller;
using ProyectM2.Gameplay.Car.Track;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerCutSceneController : MonoBehaviour
    {
        [SerializeField] private TrackController _trackController;
        [SerializeField] private AnimationController _animationController;

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
            StartCoroutine(CO_Wait());
        }

        private IEnumerator CO_Wait()
        {
            yield return new WaitForSecondsRealtime(.5f);
            _animationController.JumpAnimation();
        }

        private void OnEnemyCutSceneStarted(object[] obj)
        {
            _trackController.SetTrackState(new TrackStateCenter(_trackController));
        }
    }
}
