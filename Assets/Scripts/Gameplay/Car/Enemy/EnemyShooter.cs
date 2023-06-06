using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemyShooter: MonoBehaviour
    {
        [SerializeField] private float _shootMaxTime;
        [SerializeField] private float _bulletSpeed = 0f;
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
                var pooler = _isFirstBullet || Random.Range(0f, 1f) >= _returnChance ? _returnBulletPooler : _bulletPooler;
                var bulletObject = pooler.GetObject();
                _isFirstBullet = false;
                var bullet = bulletObject.GetComponent<Bullet>();
                bullet.SetBehaviour(new ForwardBulletBehaviour(bullet.transform,_bulletSpeed), pooler == _returnBulletPooler);
                StartCoroutine(WaitSpeedBullet());
                bullet.SetPool(pooler);
                bulletObject.transform.parent = null;
                bulletObject.transform.forward = transform.forward;
                bulletObject.transform.position = transform.position;
                _shootTime = 0;
            }
        }
        private IEnumerator WaitSpeedBullet()
        {
            yield return new WaitForSeconds(2);
            _bulletSpeed = 5;
        }
    }
}