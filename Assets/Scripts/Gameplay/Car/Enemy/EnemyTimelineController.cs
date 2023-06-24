using System.Collections;
using ProyectM2.Assets.Scripts;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemyTimelineController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PathController enemyPathController;
        [SerializeField] private PlayableDirector _playableDirector;
        [Header("Config")]
        [SerializeField] private float _range = 1f;
        [SerializeField] private float _secondsToStartAnimation = 4f;
        [SerializeField] private string _targetTag = "PathTarget";
        public UnityEvent EnemyInitializeBeforeShow;
        public UnityEvent EnemyCutSceneEnded;

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
        }

        private void OnDisable()
        {
            EventManager.StopListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
        }

        private void OnEnemyCutSceneStarted(object[] obj)
        {
            StartCoroutine(CO_EnemyTimeline());
        }

        private IEnumerator CO_EnemyTimeline()
        {
            yield return new WaitForSeconds(_secondsToStartAnimation);
            InitializeEnemy();
            yield return new WaitForSeconds((float)_playableDirector.duration);
            EventManager.TriggerEvent("EnemyCutSceneEnded");
            EnemyCutSceneEnded?.Invoke();
        }

        public void InitializeEnemy()
        {
            enemyPathController.transform.position =
                GameManager.player.transform.position + GameManager.player.transform.forward * _range;

            var closestPathTarget =
                Utility.GetClosestObjectWithTag(enemyPathController.transform.position, _targetTag);

            if (closestPathTarget != null)
            {
                enemyPathController.SetCurrentPathTarget(closestPathTarget);
                enemyPathController.transform.forward = transform.forward;
                EnemyInitializeBeforeShow?.Invoke();
                _playableDirector.Play();
            }
        }
    }
}