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
         bool _canMove;

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
        public bool OnBulletSpeed()
        {
            if (!_canMove) _canMove = true;
            return _canMove;
        }
        private void OnEnable()
        {
            _time = 0;
        }

        private void Update()
        {
            if (!_canMove) return;
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
                _canMove = false;
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
