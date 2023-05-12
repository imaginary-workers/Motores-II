using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class ForwardBulletBehaviour : IBulletBehaviour
    {
        private readonly float _destroy;
        private readonly Transform _transform;
        private readonly float _velocity;
        private readonly ObjectPool _pooler;
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
        //TODO Seria bueno si no usa el trigger separar el comportamiento en otro componente (?
        public void OnTriggerEnter(Collider other)
        {
        }
    }
}