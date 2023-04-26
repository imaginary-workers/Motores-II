using System;
using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class PlayerFiresBackController: MonoBehaviour
    {
        [SerializeField] private Transform _firesBackPointChecker;
        [SerializeField] private float _firesBackRadiusChecker = 1f;
        [SerializeField] private LayerMask _checkerLayer;
        [SerializeField] private float _maxTimeToFiresBack = 1f;
        private Bullet _returnableBullet;
        private float _timeToFiresBack;

        private void OnEnable()
        {
                //TODO Suscribirse InputManager.CurrentInput.Circulito
        }

        private void OnDisable()
        {
                //TODO Desuscribirse InputManager.CurrentInput.Circulito
        }

        private void Update()
        {
            if (_returnableBullet == null)
            {
                var colliders = Physics.OverlapSphere(_firesBackPointChecker.position, _firesBackRadiusChecker, _checkerLayer);
                if (colliders.Length == 0) return;

                foreach (var collider in colliders)
                {
                    var component = collider.GetComponent<Bullet>();
                    if (component == null) continue;
                    _returnableBullet = component;
                    _timeToFiresBack = 0f;
                    break;
                }
            }
            else
            {
                _timeToFiresBack += Time.deltaTime;
                if (_timeToFiresBack >= _maxTimeToFiresBack)
                {
                    _returnableBullet = null;
                    EventManager.TriggerEvent("PlayerGetHit");
                }
            }
            var bullet = gameObject;
            bullet.GetComponent<Bullet>()?.SetBehaviour(new SeekBulletBehaviour());
        }

        public void FireBack()
        {
            if (_)
        }
    }
}