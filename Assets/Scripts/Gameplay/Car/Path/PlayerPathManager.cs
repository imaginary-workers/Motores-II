using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    public class PlayerPathManager: PathManager
    {
        private bool _itHasGas = false;
        [SerializeField] private float _break = 5f;

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
                if (distance < closestDistance)
                {
                    closest = target;
                    closestDistance = distance;
                }
            }

            return closest;
        }

        private void OnEnable()
        {
            EventManager.StartListening("StartGameOver", OnGameOverHandler);
        }

        private void OnGameOverHandler(object[] obj)
        {
            if (obj.Length <= 0) return;
            var gameOver = (GameOver)obj[0];
            if (gameOver == GameOver.Gas)
            {
                _itHasGas = true;
                return;
            }

            enabled = false;
        }

        private void LateUpdate()
        {
            if (!_itHasGas) return;
            _speed -= _break * Time.deltaTime;
            if (_speed <= 0)
            {
                EventManager.TriggerEvent("EndGameOver", GameOver.Gas);
                enabled = false;
            }
        }

        private void OnDisable()
        {
            EventManager.StopListening("StartGameOver", OnGameOverHandler);
        }
    }
}