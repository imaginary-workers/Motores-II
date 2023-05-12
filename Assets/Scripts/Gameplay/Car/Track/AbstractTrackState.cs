using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Track
{
    public abstract class AbstractTrackState: ITrackState
    {
        protected TrackController _controller;

        public AbstractTrackState(TrackController controller)
        {
            _controller = controller;
        }

        public abstract void MoveRight();
        public abstract void MoveLeft();
        public Vector3 Track { get; set; } = Vector3.zero;

        public void Update()
        {
            if (Vector3.Distance(_controller.transform.localPosition, Track) < 0.01f) return;
            _controller.transform.localPosition = Vector3.Lerp(_controller.transform.localPosition, Track, _controller.SpeedHorizontal * Time.deltaTime);
        }
    }
}