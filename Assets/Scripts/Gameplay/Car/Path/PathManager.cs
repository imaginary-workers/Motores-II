using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] protected float _speed = 1;
        [SerializeField] protected string _targetTag = "PathTarget";
        [SerializeField] protected GameObject _currentPathTarget;
        protected bool _canMove = true;

        public void SetCurrentPathTarget(GameObject target)
        {
            _currentPathTarget = target;
            SetForwardToTarget(_currentPathTarget);
        }

        protected void SetForwardToTarget(GameObject pathTarget)
        {
            transform.forward = (pathTarget.transform.position - transform.position).normalized;
        }

        private void Update()
        {
            if (!_canMove || _currentPathTarget == null) return;
            transform.position += (_currentPathTarget.transform.position - transform.position).normalized *
                                  _speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_targetTag))
            {
                _currentPathTarget = other.GetComponent<PathTargetInfo>().NextPathTarget;
                if (_currentPathTarget == null)
                {
                    //TODO es el ultimo, así que debe avisar que terminó el nivel.
                    _canMove = false;
                    return;
                }
                SetForwardToTarget(_currentPathTarget);
            }
        }
    }
}

