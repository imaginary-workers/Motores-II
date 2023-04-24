using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class EnemyShooter: MonoBehaviour
    {
        [SerializeField] private float _shootMaxTime;
        [SerializeField] private GameObject _bulletPrefab;
        private float _shootTime = 0;
        private ObjectPool _bulletPooler;

        private void Awake()
        {
            _bulletPooler = new ObjectPool(_bulletPrefab, 2, transform);
        }

        private void Update()
        {
            _shootTime += Time.deltaTime;
            if (_shootTime >= _shootMaxTime)
            {
                // Shoot
                var bulletObject = _bulletPooler.GetObject();
                bulletObject.transform.parent = null;
                bulletObject.transform.forward = transform.forward;
                bulletObject.transform.position = transform.position;
                _shootTime = 0;
            }
        }
    }
}