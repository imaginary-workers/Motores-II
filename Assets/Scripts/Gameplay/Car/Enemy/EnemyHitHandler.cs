using ProyectM2.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemyHitHandler : MonoBehaviour
    {
        [SerializeField] EnemyDamageable _enemyDamageable;
        [SerializeField] private AnimationController _animationController;

        private void OnEnable()
        {
            _enemyDamageable.Suscribe(Hit);
        }

        private void Hit()
        {
            //TODO Cambiar la animacion de dano temporal por una fija
            _animationController.TurnLeftAnimation();
        }

        private void OnDisable()
        {
            _enemyDamageable.Unsuscribe(Hit);
        }
    }
}