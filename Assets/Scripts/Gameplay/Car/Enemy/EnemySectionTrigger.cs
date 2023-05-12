using UnityEngine;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemySectionTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.TriggerEvent("EnemyCutSceneStarted", _enemy);
            }
        }
    }
}
