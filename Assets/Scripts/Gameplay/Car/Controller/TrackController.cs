using System;
using ProyectM2.Gameplay.Car.Track;
using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Controller
{
    public class TrackController : MonoBehaviour, IActivatable, ITrackState
    {
        [SerializeField] private DataCar _dataCar;
        protected AbstractTrackState _trackState;
        private bool _isActive = false;
        private event Action<string> _observers;

        private void Awake()
        {
            _trackState = new TrackStateCenter(this);
        }

        protected virtual void Update()
        {
            if (!_isActive) return;
            _trackState.Update();
        }

        private void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            ScreenManager.Instance.Unsubscribe(this);
        }

        public float SpeedHorizontal { get => _dataCar.speedHorizontal; }

        public float HorizontalRange { get => _dataCar.horizontalRange; }

        public void MoveRight()
        {
            if (!_isActive) return;
            if (_trackState.CanMoveRight())
            {
                _trackState.MoveRight();
                NotifyToObservers("Right");
            }
        }

        public void MoveLeft()
        {
            if (!_isActive) return;
            if (_trackState.CanMoveLeft())
            {
                _trackState.MoveLeft();
                NotifyToObservers("Left");
            }
        }

        public bool CanMoveRight() => _trackState.CanMoveRight();

        public bool CanMoveLeft() => _trackState.CanMoveLeft();

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

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }
    }
}