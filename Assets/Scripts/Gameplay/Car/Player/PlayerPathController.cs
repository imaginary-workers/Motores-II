using ProyectM2.Assets.Scripts;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerPathController: PathController
    {
        private void Start()
        {
            SetCurrentPathTarget(GetClosestPathTarget());
        }
        public GameObject GetClosestPathTarget()
        {
            return Utility.GetClosestObjectWithTag(transform.position, targetTag);
        }
    }
}