using ProyectM2.Car;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private AnimManager _animManager;
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
