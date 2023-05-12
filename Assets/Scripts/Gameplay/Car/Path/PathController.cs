using ProyectM2.Gameplay.Car.Controller;
using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    public class PathController : MonoBehaviour
    {
        [SerializeField] private DataCar _dataCar;
        [SerializeField] private string _targetTag = "PathTarget";
        [SerializeField] private GameObject _currentPathTarget;
        [SerializeField] private MoveController _moveController;
        private Vector3 _targetForward;

        private void Awake()
        {
            _moveController.Speed = _dataCar.speedForward;
        }

        public void SetCurrentPathTarget(GameObject target)
        {
            _currentPathTarget = target;
            SetForwardToTarget(_currentPathTarget);
        }

        private void SetForwardToTarget(GameObject pathTarget)
        {
            _targetForward = (pathTarget.transform.position - transform.position).normalized;
        }

        private void Update()
        {
            if (_currentPathTarget == null) return;
            _moveController.Direction = (_currentPathTarget.transform.position - transform.position).normalized;
            if (Vector3.Angle(_targetForward, transform.forward) <= 0.01f) return;
            Quaternion targetRotation = Quaternion.LookRotation(_targetForward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _dataCar.speedRotation * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_targetTag))
            {
                _currentPathTarget = other.GetComponent<PathTargetInfo>().NextPathTarget;
                if (_currentPathTarget == null)
                {
                    _moveController.Speed = 0f;
                    return;
                }
                SetForwardToTarget(_currentPathTarget);
            }
        }
    }
}

