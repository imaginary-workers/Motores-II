using ProyectM2.Car;
using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public abstract class TrackController : MonoBehaviour
    {
        [SerializeField] protected DataCar data;
        [SerializeField] protected int track = 0;
        public int Track => track;
        protected Vector3 _target;

        [SerializeField] protected AnimManager myAnim;

        protected virtual void Update()
        {
            if (Vector3.Distance(transform.localPosition, _target) < 0.01f) return;
            transform.localPosition = Vector3.Lerp(transform.localPosition, _target, data.speed * Time.deltaTime);

        }
        public void MoveRight()
        {
            if (track + 1 > 1) return;
            track++;
            MoveToTrack();
            myAnim.TurnRightAnimation();
        }

        public void MoveLeft()
        {
            if (track - 1 < -1) return;
            track--;
            MoveToTrack();
            myAnim.TurnLeftAnimation();
        }

        private void MoveToTrack()
        {
            switch (track)
            {
                case -1:
                    _target = Vector3.zero + (Vector3.left * data.horizontalRange);
                    break;
                case 1:
                    _target = Vector3.zero + (Vector3.right * data.horizontalRange);
                    break;
                default:
                    _target = Vector3.zero;
                    break;
            }
        }
    }
}