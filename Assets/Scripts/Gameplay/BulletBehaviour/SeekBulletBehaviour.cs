using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class SeekBulletBehaviour: IBulletBehaviour
    {
        private readonly Transform _target;
        private readonly float _speed;
        private readonly ObjectPool _pooler;
        private readonly Transform _transform;

        public SeekBulletBehaviour(Transform transform, Transform target, float speed, ObjectPool pooler = null)
        {
            _transform = transform;
            _target = target;
            _speed = speed;
            _pooler = pooler;
        }

        public void Update()
        {
            _transform.position += (_target.position - _transform.position).normalized * _speed * Time.deltaTime;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _target.gameObject)
            {
                other.GetComponent<IDamageable>()?.TakeDamage();
                _transform.gameObject.SetActive(false);
            }
        }
    }
}