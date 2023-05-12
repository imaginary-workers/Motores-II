using System.Collections;
using ProyectM2.Assets.Scripts;
using ProyectM2.Gameplay.Car.Enemy;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;
using UnityEngine.Playables;

namespace ProyectM2.Gameplay
{
    public class EnemyTimelineController : MonoBehaviour
    {
        [SerializeField] private float _range = 1f;
        [SerializeField] private PathController enemyPathController;
        [SerializeField] private EnemyShooter _enemyShooter;
        [SerializeField] private IABehaviourMovable _enemyTrackController;
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private float _secondsToStartAnimation = 4f;
        [SerializeField] private string _targetTag = "PathTarget";
        public float distancia = 10f;
        public float ancho = 1f;
        public float alto = 1f;

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
            if (enemyPathController == null) return;
            StartCoroutine(CO_EnemyTimeline());
        }

        private IEnumerator CO_EnemyTimeline()
        {
            yield return new WaitForSeconds(_secondsToStartAnimation);
            InitializeEnemy();
            yield return new WaitForSeconds((float) _playableDirector.duration);
            EventManager.TriggerEvent("EnemyCutSceneEnded");
            _enemyShooter.enabled = true;
            _enemyTrackController.enabled = true;
        }

        public void InitializeEnemy()
        {
            enemyPathController.transform.position = GameManager.player.transform.position + GameManager.player.transform.forward * _range;

            GameObject closestPathTarget = Utility.GetClosestObjectWithTag(enemyPathController.transform.position, _targetTag);

            //var hits = Physics.BoxCastAll(enemyPathController.transform.position, new Vector3(ancho / 2, alto / 2, distancia / 2),
            //    enemyPathController.transform.forward);

            //if (hits.Length > 0)
            //{
            //    var minDistance = Mathf.Infinity;
            //    GameObject closestPathTarget = null;

            //    foreach (RaycastHit hit in hits)
            //    {
            //        if (hit.transform.CompareTag("PathTarget"))
            //        {
            //            if (hit.distance < minDistance)
            //            {
            //                minDistance = hit.distance;
            //                closestPathTarget = hit.collider.gameObject;
            //            }
            //        }
            //    }

            if (closestPathTarget != null)
                {
                    enemyPathController.SetCurrentPathTarget(closestPathTarget);
                    enemyPathController.transform.forward = transform.forward;
                    _enemyShooter.enabled = false;
                    _enemyTrackController.enabled = false;
                    _playableDirector.Play();
                }
            }
        }
    }
}
