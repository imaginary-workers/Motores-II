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

        public override bool MoveRight()
        {
            _controller.SetTrackState(new TrackStateRight(_controller));
            return true;
        }

        public override bool MoveLeft()
        {
            _controller.SetTrackState(new TrackStateLeft(_controller));
            return true;
        }
    }
}