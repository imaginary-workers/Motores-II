using ProyectM2.Assets.Scripts;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerPathController: PathController
    {
        private void OnEnable()
        {
            EventManager.StartListening("StartGameOver", OnGameOver);
        }
        
        private void OnDisable()
        {
            EventManager.StopListening("StartGameOver", OnGameOver);
        }

        private void OnGameOver(object[] obj)
        {
            _moveController.Speed = 0;
        }

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