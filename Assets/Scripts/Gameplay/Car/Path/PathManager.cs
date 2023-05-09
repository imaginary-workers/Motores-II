using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ProyectM2.Gameplay.Car.Path
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] protected float _speed = 1;
        [SerializeField] protected float _speedRotation = 1;
        [SerializeField] protected string _targetTag = "PathTarget";
        [SerializeField] protected GameObject _currentPathTarget;
        protected Vector3 _targetForward;
        protected bool _canMove = true;

        public void SetCurrentPathTarget(GameObject target)
        {
            _currentPathTarget = target;
            SetForwardToTarget(_currentPathTarget);
        }

        protected void SetForwardToTarget(GameObject pathTarget)
        {
            _targetForward = (pathTarget.transform.position - transform.position).normalized;
        }

        private void Update()
        {
            if (!_canMove || _currentPathTarget == null) return;
            transform.position += (_currentPathTarget.transform.position - transform.position).normalized *
                                  _speed * Time.deltaTime;
            if (Vector3.Angle(_targetForward, transform.forward) <= 0f) return;
            transform.forward = Vector3.Lerp(transform.forward, _targetForward, _speedRotation * Time.deltaTime);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_targetTag))
            {
                _currentPathTarget = other.GetComponent<PathTargetInfo>().NextPathTarget;
                if (_currentPathTarget == null)
                {
                    
                    _canMove = false;
                    return;
                }
                SetForwardToTarget(_currentPathTarget);
            }
        }
    }
}

