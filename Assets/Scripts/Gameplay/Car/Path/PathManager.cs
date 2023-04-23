using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] float _speed = 1;
        [SerializeField] private string _targetTag = "PathTarget";
        private bool _canMove = true;
        [SerializeField] private GameObject _currentPathTarget;

        private void Start()
        {
            _currentPathTarget = GetClosestPathTarget();
            SetForwardToTarget(_currentPathTarget);
        }

        private void SetForwardToTarget(GameObject pathTarget)
        {
            transform.forward = (pathTarget.transform.position - transform.position).normalized;
        }

        private void Update()
        {
            if (!_canMove) return;
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

        private GameObject GetClosestPathTarget()
        {
            var pathTargets = GameObject.FindGameObjectsWithTag(_targetTag);
            GameObject closest = null;
            var closestDistance = Mathf.Infinity;
            foreach (var target in pathTargets)
            {
                var distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closest = target;
                    closestDistance = distance;
                }
            }
            return closest;
        }
    }
}

