using UnityEngine;
using Random = UnityEngine.Random;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemyShooter: MonoBehaviour
    {
        [SerializeField] private float _shootMaxTime;
        [SerializeField] private GameObject _damagingBulletPrefab;
        [SerializeField] private GameObject _returnBulletPrefab;
        [SerializeField, Range(0f, 1f)] private float _returnChance;
        private float _shootTime = 0;
        private ObjectPool _bulletPooler;
        private ObjectPool _returnBulletPooler;
        private bool _isFirstBullet = true;

        private void Awake()
        {
            _bulletPooler = new ObjectPool(_damagingBulletPrefab, 2, transform);
            _returnBulletPooler = new ObjectPool(_returnBulletPrefab, 2, transform);
        }

        private void Update()
        {
            _shootTime += Time.deltaTime;
            if (_shootTime >= _shootMaxTime)
            {
                GameObject bulletObject;
                if (Random.Range(0f, 1f) >= _returnChance && !_isFirstBullet)
                {
                    bulletObject = _bulletPooler.GetObject();
                    bulletObject.GetComponent<Bullet>()?.SetBehaviour(new ForwardBulletBehaviour(5, bulletObject.transform, 5, _bulletPooler));
                }
                else
                {
                    _isFirstBullet = false;
                    bulletObject = _returnBulletPooler.GetObject();
                    bulletObject.GetComponent<Bullet>()?.SetBehaviour(new ForwardBulletBehaviour(5, bulletObject.transform, 5, _bulletPooler), true);
                }
                bulletObject.transform.parent = null;
                bulletObject.transform.forward = transform.forward;
                bulletObject.transform.position = transform.position;
                _shootTime = 0;
            }
        }
    }
}