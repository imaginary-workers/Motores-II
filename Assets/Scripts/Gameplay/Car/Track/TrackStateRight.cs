using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Track
{
    public class TrackStateRight : AbstractTrackState
    {
        public TrackStateRight(TrackController controller) : base(controller)
        {
            Track = Vector3.zero + (Vector3.right * _controller.HorizontalRange);
        }

        public override bool MoveRight() => false;

        public override bool MoveLeft()
        {
            _controller.SetTrackState(new TrackStateCenter(_controller));
            return true;
        }
    }
}