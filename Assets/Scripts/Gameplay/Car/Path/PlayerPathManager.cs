using System;
using System.Collections;
using UnityEngine;
using ProyectM2.Persistence;
using Unity.VisualScripting;

namespace ProyectM2.Gameplay.Car.Path
{
    public class PlayerPathManager: PathManager
    {
        private bool _isSlowingDown;
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
                Debug.Log("<color=yellow>"+ target.name+" -> distance: ("+ distance+")</color>");
                if (distance < closestDistance)
                {
                    closest = target;
                    closestDistance = distance;
                }
            }

            Debug.Log("<color=red>" + closest.name + "</color>");
            return closest;
        }

        private void OnEnable()
        {
            EventManager.StartListening("StartGameOver", OnGameOverHandler);
        }

        private void OnGameOverHandler(object[] obj)
        {
            if (obj.Length <= 0) return;
            if ((GameOver)obj[0] != GameOver.Gas) return;
            _isSlowingDown = true;
        }

        private void LateUpdate()
        {
            if (!_isSlowingDown) return;
            _speed -= _break * Time.deltaTime;
            if (_speed <= 0)
            {
                EventManager.TriggerEvent("EndGameOver");
                enabled = false;
            }
        }

        private void OnDisable()
        {
            EventManager.StopListening("StartGameOver", OnGameOverHandler);
        }
    }
}