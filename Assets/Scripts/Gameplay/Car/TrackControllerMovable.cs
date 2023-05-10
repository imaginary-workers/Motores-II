using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class TrackControllerMovable : TrackControllerObstacle
    {
        [SerializeField] float raycastDistance = 1f;
        [SerializeField] LayerMask layerMask;
        [SerializeField] GameObject _right;
        [SerializeField] GameObject _left;
        [SerializeField] GameObject _forward;
        protected float _time;
        [SerializeField] protected int _maxTime;

        private void Start()
        {
            _time = 0;
        }
        protected virtual void Update()
        {

            base.Update();

            var hasHitRight = false;
            var hasHitLeft = false;
            var hasFordward = false;

            _time += Time.deltaTime;

            RaycastHit hitInfoForward;
            Ray rayForward = new Ray(_forward.transform.position, transform.forward);
            if (Physics.Raycast(rayForward, out hitInfoForward, raycastDistance, layerMask))
            {
                hasFordward = hitInfoForward.transform.gameObject != transform.GetChild(0).gameObject;
            }
            else
            {
                hasFordward = false;
            }

            if (_forward != null)
            {
                if (hasFordward)
                {
                    int change = Random.Range(0, 2);
                    if (change == 0) MoveRight();
                    else MoveLeft();
                    _time = 0;
                    return;
                }
            }

            if (_time >= _maxTime)
            {
                RaycastHit hitInfoRight;
                Ray ray = new Ray(_right.transform.position, _right.transform.forward);
                if (Physics.Raycast(ray, out hitInfoRight, raycastDistance, layerMask))
                {
                    hasHitRight = hitInfoRight.transform.gameObject != transform.GetChild(0).gameObject;
                }

                RaycastHit hitInfoLeft;
                ray = new Ray(_left.transform.position, _left.transform.forward);
                if (Physics.Raycast(ray, out hitInfoLeft, raycastDistance, layerMask))
                {
                    hasHitLeft = hitInfoLeft.transform.gameObject != transform.GetChild(0).gameObject;
                }

                _time = 0;

                if (!hasHitRight && !hasHitLeft && !hasFordward)
                {
                    int change = Random.Range(0, 2);
                    if (change == 0) MoveRight();
                    else MoveLeft();
                }
                else if (hasHitRight)
                {
                    MoveLeft();
                }
                else if (hasHitLeft)
                {
                    MoveRight();
                }
            }

        }
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
    }
}
