using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Track
{
    public class TrackStateCenter : AbstractTrackState
    {
        public TrackStateCenter(TrackController controller) : base(controller)
        {
            Track = Vector3.zero;
        }

        public override void MoveRight()
        {
            _controller.SetTrackState(new TrackStateRight(_controller));
        }

        public override void MoveLeft()
        {
            _controller.SetTrackState(new TrackStateLeft(_controller));
        }

        public override bool CanMoveRight() => true;
        public override bool CanMoveLeft() => true;
    }
}