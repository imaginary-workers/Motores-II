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
            CutSceneManager.Instance.Subscribe("EnemyArrival", CutSceneState.Started, OnEnemyArrivalCutSceneStarted);
            CutSceneManager.Instance.Subscribe("EnemyDied", CutSceneState.Started, OnEnemyDiedCutSceneHandler);
        }

        private void OnDisable()
        {
            CutSceneManager.Instance.Unsubscribe("EnemyArrival", CutSceneState.Started, OnEnemyArrivalCutSceneStarted);
            CutSceneManager.Instance.Unsubscribe("EnemyDied", CutSceneState.Started, OnEnemyDiedCutSceneHandler);
        }

        private void OnEnemyDiedCutSceneHandler()
        {
            StartCoroutine(CO_Wait());
        }

        private IEnumerator CO_Wait()
        {
            yield return new WaitForSecondsRealtime(.5f);
            _animationController.JumpAnimation();
        }

        private void OnEnemyArrivalCutSceneStarted()
        {
            _trackController.SetTrackState(new TrackStateCenter(_trackController));
        }
    }
}
