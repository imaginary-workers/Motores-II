using System.Collections;
using ProyectM2.Gameplay.Car;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;
using UnityEngine.Playables;

namespace ProyectM2.Gameplay
{
    public class EnemyTimelineController : MonoBehaviour
    {
        [SerializeField] private float _range = 1f;
        [SerializeField] private PathManager _enemyPathManager;
        [SerializeField] private EnemyShooter _enemyShooter;
        [SerializeField] private TrackControllerMovable _enemyTrackController;
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private float _secondsToStartAnimation = 4f;
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
            if (_enemyPathManager == null) return;
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
            _enemyPathManager.transform.position = GameManager.player.transform.position + GameManager.player.transform.forward * _range;

            var hits = Physics.BoxCastAll(_enemyPathManager.transform.position, new Vector3(ancho / 2, alto / 2, distancia / 2),
                _enemyPathManager.transform.forward);

            if (hits.Length > 0)
            {
                var minDistance = Mathf.Infinity;
                GameObject closestPathTarget = null;

                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.CompareTag("PathTarget"))
                    {
                        if (hit.distance < minDistance)
                        {
                            minDistance = hit.distance;
                            closestPathTarget = hit.collider.gameObject;
                        }
                    }
                }
                Debug.Log(closestPathTarget);

                if (closestPathTarget != null)
                {
                    _enemyPathManager.SetCurrentPathTarget(closestPathTarget);
                    _enemyPathManager.transform.forward = transform.forward;
                    _enemyShooter.enabled = false;
                    _enemyTrackController.enabled = false;
                    _playableDirector.Play();
                }
            }
        }
    }
}
