using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class TrackControllerMovable : MonoBehaviour
    {
        [SerializeField] private TrackController _trackController;
        [SerializeField] float raycastDistance = 1f;
        [SerializeField] LayerMask layerMask;
        [SerializeField] GameObject _right;
        [SerializeField] GameObject _left;
        [SerializeField] GameObject _forward;
        protected float _time;
        [SerializeField] protected int _maxTime;
        private bool _hasHitRight = false;
        private bool _hasHitLeft = false;
        private bool _hasFordward = false;
        private RaycastHit _hitInfo;
        private Ray _ray;

        private void Start()
        {
            _time = 0;
        }
        protected virtual void Update()
        {
            _time += Time.deltaTime;

            if (CheckNierObjects(_forward.transform))
            {
                _time = 0;
                if (!CheckNierObjects(_right.transform))
                    _trackController.MoveRight();
                else if (!CheckNierObjects(_left.transform))
                    _trackController.MoveLeft();
                else
                    //TODO hacer que se detenga

                    // if (Random.Range(0, 2) == 0) _trackController.MoveRight();
                    // else _trackController.MoveLeft();
                return;
            }

            if (_time >= _maxTime)
            {
                _time = 0;
                _hasHitRight = CheckNierObjects(_right.transform);
                _hasHitLeft = CheckNierObjects(_left.transform);

                if (!_hasHitRight && !_hasHitLeft)
                {
                    int change = Random.Range(0, 2);
                    if (change == 0) _trackController.MoveRight();
                    else _trackController.MoveLeft();
                }
                else if (_hasHitRight)
                {
                    _trackController.MoveLeft();
                }
                else if (_hasHitLeft)
                {
                    _trackController.MoveRight();
                }
            }
        }

        private bool CheckNierObjects(Transform origin)
        {
            _ray = new Ray(origin.position, origin.forward);
            if (Physics.Raycast(_ray, out _hitInfo, raycastDistance, layerMask))
            {
                return _hitInfo.transform.gameObject != transform.GetChild(0).gameObject;
            }

            return false;
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
