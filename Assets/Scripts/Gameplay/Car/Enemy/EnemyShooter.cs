using ProyectM2.Sound;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class EnemyShooter : MonoBehaviour, IActivatable
    {
        [SerializeField] private float _shootMaxTime;
        [SerializeField] private float _bulletSpeed = 0f;
        [SerializeField] private GameObject _damagingBulletPrefab;
        [SerializeField] private GameObject _returnBulletPrefab;
        [SerializeField] private GameObject _gasBulletPrefab;
        [SerializeField, Range(0f, 1f)] private float _returnChance;
        [SerializeField] private float _timeMaxIsMove;
        [SerializeField] private EnemyEngineSound _soundController;
        private float _shootTime = 0;
        private ObjectPool _bulletPooler;
        private ObjectPool _returnBulletPooler;
        private ObjectPool _gasBulletPoller;
        private bool _isFirstBullet = true;
        private float _time;
        private bool _isShooting = false;
        private GameObject bulletObject;
        private Bullet bullet;
        private bool _isActive = true;
        private bool _hasToShootGas = false;
        private bool _canShootGas = true;

        private void Awake()
        {
            _bulletPooler = new ObjectPool(_damagingBulletPrefab, 2, transform);
            _returnBulletPooler = new ObjectPool(_returnBulletPrefab, 2, transform);
            _gasBulletPoller = new ObjectPool(_gasBulletPrefab, 1, transform);
        }

        private void OnEnable()
        {
            EventManager.StartListening("GiveGas", ShootGas);
            ScreenManager.Instance.Subscribe(this);
        }

        private void ShootGas(object[] obj)
        {
            _hasToShootGas = true;
        }

        private void OnDisable()
        {
            EventManager.StopListening("GiveGas", ShootGas);
            ScreenManager.Instance.Unsubscribe(this);
        }

        private void Update()
        {
            if (!_isActive) return;
            if (!_isShooting)
            {
                _shootTime += Time.deltaTime;
                if (_shootTime >= _shootMaxTime)
                {
                    _isShooting = true;
                    ObjectPool pooler;
                    if (_hasToShootGas && _canShootGas)
                    {
                        pooler = _gasBulletPoller;
                        _canShootGas = false;
                        bulletObject = pooler.GetObject();
                    }
                    else
                    {
                        pooler = _isFirstBullet || Random.Range(0f, 1f) >= _returnChance
                            ? _returnBulletPooler
                            : _bulletPooler;
                        bulletObject = pooler.GetObject();
                        _isFirstBullet = false;
                        bullet = bulletObject.GetComponent<Bullet>();
                        bullet.SetBehaviour(new ForwardBulletBehaviour(bullet.transform, _bulletSpeed),
                            pooler == _returnBulletPooler);
                        bullet.SetPool(pooler);
                    }
                    _shootTime = 0;
                }
            }
            else
            {
                bulletObject.transform.forward = transform.forward;
                bulletObject.transform.position = transform.position;
                _time += Time.deltaTime;
                if (_time > _timeMaxIsMove)
                {
                    bulletObject.transform.parent = null;
                    bullet.Shoot();
                    if (bullet.IsReturnable)
                    {
                        _soundController.PlayShootingRetornable();
                    }
                    else _soundController.PlayShootingDamaging();

                    _isShooting = false;
                    _time = 0;
                }
            }
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }
    }
}