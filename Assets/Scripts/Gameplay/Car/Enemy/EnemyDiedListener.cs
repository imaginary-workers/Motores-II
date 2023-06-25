using ProyectM2.Car.Controller;
using ProyectM2.Gameplay.Car.Controller;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemyDiedListener: MonoBehaviour
    {
        [SerializeField] private PathController _pathController;
        [SerializeField] private EnemyShooter _enemyShooter;
        [SerializeField] private TrackController _trackController;
        [SerializeField] private AnimationController _animationController;

        private void OnEnable()
        {
            CutSceneManager.Instance.Subscribe("EnemyDied", CutSceneState.Started, OnEnemyDiedCutSceneStartedHandler);
        }

        private void OnDisable()
        {
            CutSceneManager.Instance.Unsubscribe("EnemyDied", CutSceneState.Started, OnEnemyDiedCutSceneStartedHandler);
        }

        private void OnEnemyDiedCutSceneStartedHandler()
        {
            _animationController.DeathAnimation();
            _pathController.enabled = false;
            _enemyShooter.enabled = false;
            _trackController.enabled = false;
        }
    }
}