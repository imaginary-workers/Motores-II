using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Track
{
    public class TrackStateLeft : AbstractTrackState
    {
        public TrackStateLeft(TrackController controller) : base(controller)
        {
            Track = Vector3.zero + (Vector3.left * _controller.HorizontalRange);
        }

        public override bool MoveRight()
        {
            _controller.SetTrackState(new TrackStateCenter(_controller));
            return true;
        }

        public override bool MoveLeft() => false;
    }
}