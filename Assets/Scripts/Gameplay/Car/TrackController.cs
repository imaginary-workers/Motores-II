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
        
        [SerializeField] protected AnimManager myAnim;
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
                    transform.localPosition += transform.localPosition + (Vector3.left * data.horizontalRange);
                    break;
                case 1:
                    transform.localPosition += transform.localPosition + (Vector3.right * data.horizontalRange);
                    break;
                default:
                    transform.localPosition = new Vector3(0, 0, 0);
                    break;
            }
        }
    }
}