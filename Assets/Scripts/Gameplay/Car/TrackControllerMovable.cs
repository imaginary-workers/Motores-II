using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class TrackControllerMovable : TrackControllerObstacle
    {
        [SerializeField] float raycastDistance = 1f;
        [SerializeField] LayerMask layerMask;
        [SerializeField] GameObject _right;
        [SerializeField] GameObject _left;
        float _time;
        [SerializeField] int _maxTime;

        private void Start()
        {
            _time = 0;
        }
        private void Update()
        {

            _time += Time.deltaTime;
            if (_time >= _maxTime)
            {
                RaycastHit hitRight, hitLeft;

                Ray ray = new Ray(_right.transform.position, _right.transform.forward);

                bool hasHitRight = Physics.Raycast(ray, raycastDistance, layerMask);


                ray = new Ray(_left.transform.position, _left.transform.forward);
                bool hasHitLeft = Physics.Raycast(ray, raycastDistance, layerMask);

                _time = 0;

                if (!hasHitRight && !hasHitLeft)
                {
                    int change = Random.Range(0, 2);
                    if (change == 0) MoveRight();
                    else MoveLeft();
                }
                else if (hasHitRight)
                {
                    Debug.Log("no puedo ir a la derecha");
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

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);

            ray = new Ray(_left.transform.position, _left.transform.forward * raycastDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);

        }
    }
}
