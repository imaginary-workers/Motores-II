using ProyectM2.Assets.Scripts;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerPathController: PathController
    {
        [SerializeField] private string _targetTag = "PathTarget";
         private void Start()
        {
            SetCurrentPathTarget(GetClosestPathTarget());
        }
        public GameObject GetClosestPathTarget()
        {
            return Utility.GetClosestObjectWithTag(transform.position, _targetTag);
            
            //var pathTargets = GameObject.FindGameObjectsWithTag(_targetTag);

            //GameObject closest = null;
            //var closestDistance = Mathf.Infinity;
            //foreach (var target in pathTargets)
            //{
            //    var distance = (target.transform.position - transform.position).magnitude;
            //    if (distance < closestDistance)
            //    {
            //        closest = target;
            //        closestDistance = distance;
            //    }
            //}

            //return closest;
        }
    }
}