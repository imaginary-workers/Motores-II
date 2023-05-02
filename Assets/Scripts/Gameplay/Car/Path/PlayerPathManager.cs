using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    public class PlayerPathManager: PathManager
    {
        private void Awake()
        {
            SetCurrentPathTarget(GetClosestPathTarget());
        }
        
        public GameObject GetClosestPathTarget()
        {
            var pathTargets = GameObject.FindGameObjectsWithTag(_targetTag);
            GameObject closest = null;
            var closestDistance = Mathf.Infinity;
            foreach (var target in pathTargets)
            {
                var distance = (target.transform.position - transform.position).magnitude;
                Debug.Log("<color=yellow>"+ target.name+" -> distance: ("+ distance+")</color>");
                if (distance < closestDistance)
                {
                    closest = target;
                    closestDistance = distance;
                }
            }

            Debug.Log("<color=red>"+ closest.name+"</color>");
            return closest;
        }
    }
}