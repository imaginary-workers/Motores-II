using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class ForwardBulletBehaviour : IBulletBehaviour
    {
        private float _destroy;
        private Transform _transform;
        private float _velocity;
        private ObjectPool _pooler;
        private float _time;

        public ForwardBulletBehaviour(float destroy, Transform transform, float velocity, ObjectPool pooler = null)
        {
            _destroy = destroy;
            _transform = transform;
            _velocity = velocity;
            _pooler = pooler;
            _time = 0f;
        }

        public void Update()
        {
            _time += Time.deltaTime;
            _transform.position += _transform.forward * _velocity * Time.deltaTime;
            if (_time >= _destroy)
            {
                if (_pooler == null)
                {
                    _transform.gameObject.SetActive(false);
                }
                else
                {
                    _pooler.ReturnObject(_transform.gameObject);
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {
        }
    }
}