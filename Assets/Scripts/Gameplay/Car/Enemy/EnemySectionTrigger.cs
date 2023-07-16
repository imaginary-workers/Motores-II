using ProyectM2.Gameplay.Car.Player;
using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemySectionTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        public UnityEvent OnEnemyCutSceneStarted;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponentInChildren<PlayerFiresBackController>().enemyTarget = _enemy;
                CutSceneManager.Instance.StartCutScene("EnemyArrival");
                OnEnemyCutSceneStarted?.Invoke();
            }
        }
    }
}
