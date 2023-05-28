using System;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _life = 3;
        private event Action _myObserver;

        [ContextMenu("Take Damage")]
        public void TakeDamage()
        {
            _life--;
            if (_life <= 0)
            {
                EventManager.TriggerEvent("EnemyDiedCutSceneStarted");
            }
            else
            {
                NotifyToObservers();
            }
        }

        public void NotifyToObservers()
        {
            _myObserver?.Invoke();
        }

        public void Suscribe(Action obs)
        {
            _myObserver += obs;
        }

        public void Unsuscribe(Action obs)
        {
            _myObserver -= obs;
        }
    }
}
