using ProyectM2.Gameplay.Car.Enemy;
using UnityEngine;

namespace ProyectM2.Sound
{
    public class EnemyEngineSound : EngineSoundController
    {
        [SerializeField] private EnemyDamageable _enemyDamageable;
        [SerializeField] private AudioClip _shootDamaging;
        [SerializeField] private AudioClip _shootRetornable;
        [SerializeField] private AudioClip _damaged;

        protected override void OnEnable()
        {
            base.OnEnable();
            _enemyDamageable.Suscribe(Damaged);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _enemyDamageable.Unsuscribe(Damaged);
        }
        public void PlayShootingDamaging()
        {
            _source.PlayOneShot(_shootDamaging);
        }

        public void PlayShootingRetornable()
        {
            _source.PlayOneShot(_shootRetornable);
        }
        public void Damaged()
        {
            _source.PlayOneShot(_damaged);
        }
    }
}
