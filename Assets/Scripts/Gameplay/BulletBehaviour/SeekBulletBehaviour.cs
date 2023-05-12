using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class SeekBulletBehaviour: IBulletBehaviour
    {
        private readonly Transform _target;
        private readonly float _speed;
        private readonly Transform _transform;

        public SeekBulletBehaviour(Transform transform, Transform target, float speed)
        {
            _transform = transform;
            _target = target;
            _speed = speed;
        }

        public void Update()
        {
            _transform.position += (_target.position - _transform.position).normalized * (_speed * Time.deltaTime);
        }
    }
}