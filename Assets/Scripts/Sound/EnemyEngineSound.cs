using ProyectM2.Gameplay.Car.Enemy;
using ProyectM2.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class EnemyEngineSound : EngineSoundController
    {
        [SerializeField] EnemyDamageable _enemyDamageable;
        [SerializeField] AudioClip _shootDamaging;
        [SerializeField] AudioClip _shootRetornable;
        [SerializeField] AudioClip _damaged;

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
