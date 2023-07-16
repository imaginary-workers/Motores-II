using ProyectM2.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemyHitHandler : MonoBehaviour
    {
        [SerializeField] private  EnemyDamageable _enemyDamageable;
        [SerializeField] private AnimationController _animationController;

        private void OnEnable()
        {
            _enemyDamageable.Suscribe(Hit);
        }

        private void Hit()
        {
            _animationController.TurnLeftAnimation();
        }

        private void OnDisable()
        {
            _enemyDamageable.Unsuscribe(Hit);
        }
    }
}