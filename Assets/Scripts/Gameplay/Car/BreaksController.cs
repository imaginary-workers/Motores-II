using ProyectM2.Assets.Scripts;
using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.Gameplay.Car
{
    public class BreaksController : MonoBehaviour
    {
        [SerializeField] private Transform _forward;
        [SerializeField] private float arriveDistance = 1f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private GameObject _thisCar;
        [SerializeField] private MoveController _moveController;
        public UnityEvent OnStartBreaking;
        private ArriveBehaviour _arriveBehaviour;
        private bool IsBreaking = false;

        private void Start()
        {
            _arriveBehaviour = new ArriveBehaviour(_moveController, transform);
        }

        private void Update()
        {
            if (!IsBreaking)
            {
                IsBreaking = Utility.CheckNierObjects(_forward, arriveDistance, layerMask, _thisCar);
                if (IsBreaking)
                    OnStartBreaking?.Invoke();
            }
            else
            {
                Vector3 directionToStop = transform.forward * -1f;
                _arriveBehaviour.Arrive(transform.position + directionToStop);
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var ray = new Ray(_forward.transform.position, _forward.transform.forward * arriveDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * arriveDistance);
        }
#endif
    }
}