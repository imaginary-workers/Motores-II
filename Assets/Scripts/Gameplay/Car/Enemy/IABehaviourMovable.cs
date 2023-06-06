using ProyectM2.Assets.Scripts;
using ProyectM2.Car.Controller;
using ProyectM2.Gameplay.Car.Controller;
using System.Collections;
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
        [SerializeField] AnimationController _ani;
        [SerializeField, Range(0.1f, 5f)] float waitTime = 2f;
        protected float _time;
        private bool _hasHitRight = false;
        private bool _hasHitLeft = false;
        private RaycastHit _hitInfo;
        private Ray _ray;
        int move;



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
                RandomMove();
                move = RandomMove();

                if (move == 1)
                {
                    _ani.LightRightAnimation();
                    StartCoroutine(WaitAndMove(_trackController.MoveRight, waitTime));
                }
                else if (move == -1)
                {
                    _ani.LightLeftAnimation();
                    StartCoroutine(WaitAndMove(_trackController.MoveLeft, waitTime));
                }
                else return;
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
        private IEnumerator WaitAndMove(System.Action moveAction, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            moveAction.Invoke();
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
