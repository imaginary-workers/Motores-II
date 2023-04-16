using ProyectM2.Managers;
using UnityEngine;

namespace ProyectM2.Car
{
    public class PlayerCar : Car
    {
        [SerializeField] float _speed = 10;
        [SerializeField, Range(1, 5)] private float _horizontalRange = 3.22f;
        private int track = 0;

        public override float Speed => _speed;
        public override void MoveRight()
        {
            if (track + 1 > 1) return;
            track++;
            MoveToTrack();
        }

        private void MoveToTrack()
        {
            switch (track)
            {
                case -1:
                    Debug.Log("LEFT");
                    transform.Translate(Vector3.left * -_horizontalRange);
                    break;
                case 1:
                    Debug.Log("Right");
                    transform.Translate(Vector3.right * _horizontalRange);
                    break;
                default:
                    transform.position = new Vector3(0, 0, 0);
                    break;
            }
        }

        public override void MoveLeft()
        {
            Debug.Log("Left");
            if (track - 1 < -1) return;
            track--;
            MoveToTrack();
        }

        public override void MoveForward()
        {
        }
        
        void OnEnable()
        {
            InputManager.CurrentInput.Horizontal += OnHorizontal;
        }

        void OnDisable()
        {
            InputManager.CurrentInput.Horizontal -= OnHorizontal;
        }

        void Update()
        {
            InputManager.CurrentInput.OnUpdate();
        }

        void OnHorizontal(int hor)
        {
            if (hor > 0)
                MoveRight();
            else
                MoveLeft();
        }
    }
}