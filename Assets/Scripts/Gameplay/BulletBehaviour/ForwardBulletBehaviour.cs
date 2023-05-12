using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class ForwardBulletBehaviour : IBulletBehaviour
    {
        private readonly Transform _transform;
        private readonly float _speed;

        public ForwardBulletBehaviour(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public void Update()
        {
            _transform.position += _transform.forward * (_speed * Time.deltaTime);
        }
    }
}