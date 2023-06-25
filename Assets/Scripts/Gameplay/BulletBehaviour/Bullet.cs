using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class Bullet : MonoBehaviour, IActivatable
    {
        [SerializeField] private float _destroyTime = 5f;
        private IBulletBehaviour _behaviour;
        private ObjectPool _pool;
        private float _time;
        private bool _canMove;

        public bool IsReturnable { get; private set; } = false;
        
        private void OnEnable()
        {
            _time = 0;
            ScreenManager.Instance.Subscribe(this);
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

        private void OnDisable()
        {
            gameObject.layer = 12;
            ScreenManager.Instance.Unsubscribe(this);
        }

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

        public void Shoot()
        {
            _canMove = true;
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

        public void Activate()
        {
            _canMove = true;
        }

        public void Deactivate()
        {
            _canMove = false;
        }
    }
}
