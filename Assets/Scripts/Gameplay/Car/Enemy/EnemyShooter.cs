﻿using System;
using ProyectM2.Sound;
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
        [SerializeField, Range(0f, 1f)] private float _returnChance;
        private float _shootTime = 0;
        private ObjectPool _bulletPooler;
        private ObjectPool _returnBulletPooler;
        private bool _isFirstBullet = true;
        float _time;
        [SerializeField] float _timeMaxIsMove;
        bool _isShooting = false;
        GameObject bulletObject;
        Bullet bullet;
        [SerializeField] EnemyEngineSound _soundController;
        private bool _isActive = true;

        private void Awake()
        {
            _bulletPooler = new ObjectPool(_damagingBulletPrefab, 2, transform);
            _returnBulletPooler = new ObjectPool(_returnBulletPrefab, 2, transform);
        }

        private void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
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
                    var pooler = _isFirstBullet || Random.Range(0f, 1f) >= _returnChance
                        ? _returnBulletPooler
                        : _bulletPooler;
                    bulletObject = pooler.GetObject();
                    _isFirstBullet = false;
                    bullet = bulletObject.GetComponent<Bullet>();
                    bullet.SetBehaviour(new ForwardBulletBehaviour(bullet.transform, _bulletSpeed),
                        pooler == _returnBulletPooler);
                    bullet.SetPool(pooler);
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