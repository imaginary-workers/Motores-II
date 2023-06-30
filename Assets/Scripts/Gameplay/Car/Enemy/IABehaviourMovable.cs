using ProyectM2.Assets.Scripts;
using ProyectM2.Car.Controller;
using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class IABehaviourMovable : MonoBehaviour, IActivatable
    {
        [Header("Dependencies")]
        [SerializeField] private TrackController _trackController;
        [SerializeField] private GameObject _right;
        [SerializeField] private GameObject _left;
        [SerializeField] private GameObject _thisCar;
        [SerializeField] private AnimationController _ani;
        [Header("Optional")]
        [SerializeField, Tooltip("Solo si se tiene que activar unicamente si esta visible")]
        private VisibilityController _visibilityController;
        [SerializeField, Tooltip("Si empieza activo -> true, sino -> false")] private bool _isActive = false;
        [Header("Config")]
        [SerializeField] private float raycastDistance = 1f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private int _maxTime;
        [FormerlySerializedAs("waitTime")] [SerializeField, Range(0.1f, 5f)]
        private float _maxWaitTime = 2f;
        private float _time;
        private bool _hasHitRight = false;
        private bool _hasHitLeft = false;
        private RaycastHit _hitInfo;
        private Ray _ray;
        private int _move;
        private float _waitingTime = 0f;

        private void Awake()
        {
            _time = 0;
        }
        private void Update()
        {
            if (!_isActive) return;
            if (_waitingTime > 0)
            {
                _waitingTime -= Time.deltaTime;
                if (_waitingTime <= 0)
                {
                    if (_move == 1)
                        _trackController.MoveRight();
                    else
                        _trackController.MoveLeft();
                }
                return;
            }
            
            _time += Time.deltaTime;

            if (_time >= _maxTime)
            {
                _time = 0;
                _move = RandomMove();

                if (_move == 0) return;
                
                _waitingTime = _maxWaitTime;
                if (_move == 1)
                {
                    _ani.RightLightAnimation();
                }
                else
                {
                    _ani.LeftLightAnimation();
                }
            }
        }

        private int RandomMove()
        {
            _hasHitRight = Utility.CheckNierObjects(_right.transform, raycastDistance, layerMask, _thisCar);
            _hasHitLeft = Utility.CheckNierObjects(_left.transform, raycastDistance, layerMask, _thisCar);
            if (!_hasHitRight && !_hasHitLeft)
            {
                int change = Random.Range(0, 2);
                if (change == 0) return 1;
                else return -1;
            }
            else if (_hasHitRight && !_hasHitLeft)
            {
                return -1;
            }
            else if (_hasHitLeft && !_hasHitRight)
            {
                return 1;
            }
            return 0;
        }

        public void Activate()
        {
            if (_visibilityController != null && !_visibilityController.IsVisible) return;
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Ray ray = new Ray(_right.transform.position, _right.transform.forward * raycastDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);

            ray = new Ray(_left.transform.position, _left.transform.forward * raycastDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);
        }
#endif
    }
}
