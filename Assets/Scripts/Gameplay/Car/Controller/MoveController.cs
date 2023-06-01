using UnityEngine;

namespace ProyectM2.Gameplay.Car.Controller
{
    public class MoveController : MonoBehaviour, ISpeedProvider
    {
        private float _speed = 0;
        private bool _canMove = true;
        private Vector3 _direction;

        private void Update()
        {
            if (_speed <= 0f) return;
            transform.position += _direction * (_speed * Time.deltaTime);
        }

        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }
    }
}