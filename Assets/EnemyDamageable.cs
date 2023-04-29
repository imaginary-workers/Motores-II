using ProyectM2.Car;
using ProyectM2.Gameplay;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;

namespace ProyectM2
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private AnimManager _animManager;
        [SerializeField] private PathManager _pathManager;
        [SerializeField] private int _life = 3;

        public void TakeDamage()
        {
            _life--;
            if (_life <= 0)
            {
                EventManager.TriggerEvent("EnemyDiedCutSceneStarted");
                _animManager.DeathAnimation();
            }
            else
            {
                _animManager.TurnLeftAnimation();
            }
        }
    }
}
