using System;
using ProyectM2.Car.Controller;
using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerFiresBackController : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _maxTimeToFiresBack = 1f;
        [SerializeField] private Camera _camera;
        [SerializeField] private AnimationController _animationManager;
        [SerializeField] private int _returnableLayer;
        [SerializeField] private float _bulletSpeed = 50f;
        protected Bullet returnableBullet;
        [NonSerialized] public GameObject enemyTarget = null;
        private float _timeToFiresBack;
        private Ray _ray;
        public event Action<Vector3> OnFireBack;

        private void Start()
        {
            if (InputManager.Instance.StrategyType == StrategyType.Button)
            {
                InputManager.Instance.OnFireBack += FireBackButtonChecker;
            }
            else
            {
                InputManager.Instance.OnClick += FireBackClickChecker;
            }
        }

        private void OnDisable()
        {
            InputManager.Instance.OnClick -= FireBackClickChecker;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var component = other.GetComponent<Bullet>();
            if (component == null || !component.IsReturnable) return;
            returnableBullet = component;
            _timeToFiresBack = Time.realtimeSinceStartup;
            Time.timeScale = .1f;
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (returnableBullet != null)
            {
                Time.timeScale = 1f;
                returnableBullet = null;
            }
        }

        public void FireBackClickChecker(Vector3 position)
        {
            if (!CanFiresBack()) return;
            _ray = _camera.ScreenPointToRay(position);
            RaycastHit hit;
            if (Physics.Raycast(_ray, out hit, Single.PositiveInfinity, _playerLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Time.timeScale = 1f;
                    FirebackAction();
                }
            }
        }

        private void FireBackButtonChecker()
        {
            if (!CanFiresBack()) return;
            FirebackAction();
        }

        private bool CanFiresBack()
        {
            if (returnableBullet == null) return false;
            if (enemyTarget == null) return false;
            if (Time.realtimeSinceStartup - _timeToFiresBack >= _maxTimeToFiresBack) return false;

            return true;
        }

        protected virtual void FirebackAction()
        {
            _animationManager.HipUpAnimation();
            returnableBullet.gameObject.layer = _returnableLayer;
            returnableBullet.SetBehaviour(new SeekBulletBehaviour(returnableBullet.transform, enemyTarget.transform,
                _bulletSpeed));
            OnFireBack?.Invoke(returnableBullet.transform.position);
            returnableBullet = null;
        }
    }
}