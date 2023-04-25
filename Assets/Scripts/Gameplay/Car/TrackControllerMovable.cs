using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class TrackControllerMovable : TrackControllerObstacle
    {
        [SerializeField] float raycastDistance = 1f;
        [SerializeField] LayerMask layerMask;
        [SerializeField] GameObject _right;
        [SerializeField] GameObject _left;
        protected float _time;
        [SerializeField] protected int _maxTime;

        private void Start()
        {
            _time = 0;
        }
        protected virtual void Update()
        {

            _time += Time.deltaTime;
            if (_time >= _maxTime)
            {
                var hasHitRight = false;
                var hasHitLeft = false;

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

                if (!hasHitRight && !hasHitLeft)
                {
                    int change = Random.Range(0, 2);
                    Debug.Log("eleji random a la " + (change == 0? "derecha" : "izquierda"));
                    if (change == 0) MoveRight();
                    else MoveLeft();
                }
                else if (hasHitRight)
                {
                    Debug.Log("no puedo ir a la derecha" );
                    MoveLeft();
                }
                else if (hasHitLeft)
                {
                    Debug.Log("no puedo ir a la izquierda");
                    MoveRight();
                }
            }
        }
        private void OnDrawGizmos()
        {
            Ray ray = new Ray(_right.transform.position, _right.transform.forward * raycastDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);

            ray = new Ray(_left.transform.position, _left.transform.forward * raycastDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);

        }
    }
}
