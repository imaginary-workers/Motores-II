using System;
using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class PlayerFiresBackController: MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _maxTimeToFiresBack = 1f;
        private Bullet _returnableBullet;
        private float _timeToFiresBack;
        [SerializeField] private Camera _camera;
        private GameObject _enemyTarget = null;
        private Ray _ray;

        private void OnEnable()
        {
                //TODO Suscribirse InputManager.CurrentInput.Circulito
                InputManager.CurrentInput.Click += FireBack;
                EventManager.StartListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
        }

        private void OnEnemyCutSceneStarted(object[] obj)
        {
            _enemyTarget = (GameObject) obj[0];
        }

        private void OnDisable()
        {
                //TODO Desuscribirse InputManager.CurrentInput.Circulito
                InputManager.CurrentInput.Click -= FireBack;
        }

        private void OnTriggerEnter(Collider other)
        {
            var component = other.GetComponent<Bullet>();
            if (component == null || !component.IsReturnable) return;
            _returnableBullet = component;
            _timeToFiresBack = 0f;
        }

        private void Update()
        {
            
            if (_timeToFiresBack < _maxTimeToFiresBack)
            {
                _timeToFiresBack += Time.deltaTime;
            }
        }

        public void FireBack(Vector3 position)
        {
            if (_returnableBullet == null) return;
            if (_enemyTarget == null) return;
            if (_timeToFiresBack >= _maxTimeToFiresBack) return;
            _ray = _camera.ScreenPointToRay(position);
            RaycastHit hit;
            if (Physics.Raycast(_ray, out hit, Single.PositiveInfinity, _playerLayer))
            {
                Debug.Log("Hit "+hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Player"))
                {
                    _returnableBullet.SetBehaviour(new SeekBulletBehaviour(_returnableBullet.transform, _enemyTarget.transform, 50));
                    _returnableBullet = null;
                }
            }
        }
    }
}