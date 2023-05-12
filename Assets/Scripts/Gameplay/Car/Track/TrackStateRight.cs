using ProyectM2.Gameplay.Car.Controller;

namespace ProyectM2.Gameplay.Car.Track
{
    public class TrackStateRight : AbstractTrackState
    {
        public TrackStateRight(TrackController controller) : base(controller)
        {
        }

        public override void MoveRight() { }

        public override void MoveLeft()
        {
            _controller.SetTrackState(new TrackStateCenter(_controller));
        }
    }
}