using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class SeekBulletBehaviour: IBulletBehaviour
    {
        private Transform _target;
        private float _speed;
        private ObjectPool _pooler;
        private Transform _transform;

        public SeekBulletBehaviour(Transform transform, Transform target, float speed, ObjectPool pooler = null)
        {
            _transform = transform;
            _target = target;
            _speed = speed;
            _pooler = pooler;
        }

        public void Update()
        {
            var direction = _target.position - _transform.position;
            direction.Normalize();
            _transform.position += direction * _speed * Time.deltaTime;
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