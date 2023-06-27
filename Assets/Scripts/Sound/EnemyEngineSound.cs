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

        private void OnEnable()
        {
            _enemyDamageable.Suscribe(Damaged);
        }
        private void OnDisable()
        {
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
