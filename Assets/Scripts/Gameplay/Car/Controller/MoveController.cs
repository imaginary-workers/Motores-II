using UnityEngine;

namespace ProyectM2.Gameplay.Car.Controller
{
    public class MoveController : MonoBehaviour, ISpeedProvider, IActivatable
    {
        [SerializeField, Tooltip("Si empieza activo -> true, sino -> false")]  private bool _canMove = false;
        [SerializeField, Tooltip("Si se quiere activar solo si esta visible")]  private VisibilityController _visibilityController;
        private float _speed = 0;
        private Vector3 _direction;

        private void Update()
        {
            if (!_canMove) return;
            if (_speed <= 0f) return;
            transform.position += _direction * (_speed * Time.deltaTime);
        }

        private void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            ScreenManager.Instance.Unsubscribe(this);
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

        public void Activate()
        {
            if (_visibilityController != null && !_visibilityController.IsVisible) return;
            _canMove = true;
        }

        public void Deactivate()
        {
            _canMove = false;
        }
    }
}