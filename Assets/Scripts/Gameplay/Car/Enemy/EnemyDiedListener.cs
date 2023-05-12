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
        private void OnEnable()
        {
            EventManager.StartListening("EnemyDiedCutSceneStarted", OnEnemyDiedCutSceneStartedHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("EnemyDiedCutSceneStarted", OnEnemyDiedCutSceneStartedHandler);
        }

        private void OnEnemyDiedCutSceneStartedHandler(object[] obj)
        {
            _pathController.enabled = false;
            _enemyShooter.enabled = false;
            _trackController.enabled = false;
        }
    }
}