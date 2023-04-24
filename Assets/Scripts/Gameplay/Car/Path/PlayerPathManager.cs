using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    public class PlayerPathManager: PathManager
    {
        private void Start()
        {
            _currentPathTarget = GetClosestPathTarget();
            SetForwardToTarget(_currentPathTarget);
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