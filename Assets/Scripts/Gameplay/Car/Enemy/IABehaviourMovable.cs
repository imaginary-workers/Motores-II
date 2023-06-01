using ProyectM2.Assets.Scripts;
using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Enemy
{
    public class IABehaviourMovable : MonoBehaviour
    {
        [SerializeField] private TrackController _trackController;
        [SerializeField] float raycastDistance = 1f;
        [SerializeField] LayerMask layerMask;
        [SerializeField] GameObject _right;
        [SerializeField] GameObject _left;
        [SerializeField] GameObject _forward;
        [SerializeField] private GameObject _thisCar;
        [SerializeField] protected int _maxTime;
        protected float _time;
        private bool _hasHitRight = false;
        private bool _hasHitLeft = false;
        private RaycastHit _hitInfo;
        private Ray _ray;
        

        private void Start()
        {
            _time = 0;
        }
        protected virtual void Update()
        {
            _time += Time.deltaTime;

            if (_time >= _maxTime)
            {
                _time = 0;
                _hasHitRight = Utility.CheckNierObjects(_right.transform, raycastDistance, layerMask, _thisCar);
                _hasHitLeft = Utility.CheckNierObjects(_left.transform, raycastDistance, layerMask, _thisCar);

                if (!_hasHitRight && !_hasHitLeft)
                {
                    int change = Random.Range(0, 2);
                    if (change == 0) _trackController.MoveRight();
                    else _trackController.MoveLeft();
                }
                else if (_hasHitRight && !_hasHitLeft)
                {
                    _trackController.MoveLeft();
                }
                else if (_hasHitLeft && !_hasHitRight)
                {
                    _trackController.MoveRight();
                }
            }
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

            ray = new Ray(_forward.transform.position, _forward.transform.forward * raycastDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);

        }
#endif
    }
}
