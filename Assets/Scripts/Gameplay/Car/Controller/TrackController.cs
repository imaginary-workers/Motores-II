using System;
using ProyectM2.Gameplay.Car.Track;
using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Controller
{
    public class TrackController : MonoBehaviour
    {
        [SerializeField] private DataCar _dataCar;
        protected AbstractTrackState _trackState;
        private event Action<string> _observers;

        private void Awake()
        {
            _trackState = new TrackStateCenter(this);
        }

        protected virtual void Update()
        {
            _trackState.Update();
        }
        
        public float SpeedHorizontal
        {
            get => _dataCar.speedHorizontal;
        }

        public float HorizontalRange { get => _dataCar.horizontalRange; }

        public void MoveRight()
        {
            _trackState.MoveRight();
            NotifyToObservers("Right");
        }

        public void MoveLeft()
        {
            _trackState.MoveLeft();
            NotifyToObservers("Left");
        }

        public void SetTrackState(AbstractTrackState state)
        {
            _trackState = state;
        }

        public void Suscribe(Action<string> obs)
        {
            _observers += obs;
        }

        public void NotifyToObservers(string notification)
        {
            _observers?.Invoke(notification);
        }

        public void Unsuscribe(Action<string> obs)
        {
            _observers -= obs;
        }
    }
}