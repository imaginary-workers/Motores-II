using System;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        private IBulletBehaviour _behaviour;
        private ObjectPool _pool;
        private float _time;
        [SerializeField] private float _destroyTime = 5f;

        public bool IsReturnable { get; private set; } = false;

        public void SetPool(ObjectPool pool)
        {
            _pool = pool;
        }

        public void SetBehaviour(IBulletBehaviour behaviour, bool returnable = false)
        {
            _behaviour = behaviour;
            IsReturnable = returnable;
            _time = 0;
        }

        private void OnEnable()
        {
            _time = 0;
        }

        private void Update()
        {
            _behaviour.Update();
            _time += Time.deltaTime;
            if (_time >= _destroyTime)
            {
                DestroyBullet();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsReturnable) return;
            other.GetComponent<IDamageable>()?.TakeDamage();
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            if (_pool == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _pool.ReturnObject(gameObject);
            }
        }

        private void OnDisable()
        {
            gameObject.layer = 12;
        }
    }
}
