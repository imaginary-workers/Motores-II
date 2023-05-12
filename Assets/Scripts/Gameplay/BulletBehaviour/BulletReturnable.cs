using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class BulletReturnable : Bullet
    {
        [SerializeField] float _velocity;
        Vector2 _position;
        [SerializeField] float _destroy;
        private ObjectPool _pooler;

        public void SetPooler(ObjectPool pooler)
        {
            _pooler = pooler;
        }

        private void OnEnable()
        {
            _destroy = 0;
        }

        private void Update()
        {
            _destroy += Time.deltaTime;
            transform.position += transform.forward * _velocity * Time.deltaTime;
            if (_destroy >= 10)
            {
                if (_pooler == null)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    _pooler.ReturnObject(gameObject);
                }
            }
        }
    }
}
