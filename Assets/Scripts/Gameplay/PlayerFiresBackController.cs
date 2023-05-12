using System;
using ProyectM2.Car;
using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class PlayerFiresBackController: MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _maxTimeToFiresBack = 1f;
        [SerializeField] private Camera _camera;
        [SerializeField] private AnimationController _animationManager;
        protected Bullet returnableBullet;
        private float _timeToFiresBack;
        private GameObject _enemyTarget = null;
        private Ray _ray;
        [SerializeField] private int _returnableLayer;

        private void OnEnable()
        {
                //TODO Suscribirse InputManager.CurrentInput.Circulito
                InputManager.CurrentInput.Click += FireBackChecker;
                EventManager.StartListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
        }

        private void OnEnemyCutSceneStarted(object[] obj)
        {
            _enemyTarget = (GameObject) obj[0];
        }

        private void OnDisable()
        {
                //TODO Desuscribirse InputManager.CurrentInput.Circulito
                InputManager.CurrentInput.Click -= FireBackChecker;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var component = other.GetComponent<Bullet>();
            if (component == null || !component.IsReturnable) return;
            returnableBullet = component;
            _timeToFiresBack = 0f;
        }

        private void Update()
        {
            
            if (_timeToFiresBack < _maxTimeToFiresBack)
            {
                _timeToFiresBack += Time.deltaTime;
            }
        }

        public void FireBackChecker(Vector3 position)
        {
            if (returnableBullet == null) return;
            if (_enemyTarget == null) return;
            if (_timeToFiresBack >= _maxTimeToFiresBack) return;
            _ray = _camera.ScreenPointToRay(position);
            RaycastHit hit;
            if (Physics.Raycast(_ray, out hit, Single.PositiveInfinity, _playerLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    FirebackAction();
                }
            }
        }

        protected virtual void FirebackAction()
        {
            _animationManager.HipUpAnimation();
            returnableBullet.gameObject.layer = _returnableLayer;
            returnableBullet.SetBehaviour(new SeekBulletBehaviour(returnableBullet.transform, _enemyTarget.transform, 50));
            returnableBullet = null;
        }
    }
}