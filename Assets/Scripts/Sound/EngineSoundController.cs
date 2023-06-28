using ProyectM2.Gameplay;
using ProyectM2.Gameplay.Car.Controller;
using ProyectM2.Gameplay.Car.Enemy;
using UnityEngine;

namespace ProyectM2.Sound
{
    public class EngineSoundController : MonoBehaviour, IActivatable
    {
        [SerializeField] AudioClip _driving;
        [SerializeField] protected AudioSource _source;
        [SerializeField] private MoveController _moveController;
        [SerializeField, Tooltip("Si empieza activo -> true, sino -> false")] private bool _isActive = false;

        private void Awake()
        {
            _source.Stop();
        }

        protected virtual void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
        }

        protected virtual void OnDisable()
        {
            ScreenManager.Instance.Unsubscribe(this);
        }

        private void Update()
        {
            if (!_isActive) return;
            if (_moveController.Speed <= 0)
            {
                StopSound();
            }
            else
            {
                RunSound();
            }
        }

        public void RunSound()
        {
            if (_source.isPlaying && _source.clip == _driving) return;
            _source.clip = _driving;
            _source.loop = true;
            _source.Play();
        }

        public void StopSound()
        {
            _source.Stop();
        }

        public void Activate()
        {
            Debug.Log("active");
            _isActive = true;
        }

        public void Deactivate()
        {
            Debug.Log("desactivado");

            _isActive = false;
            StopSound();
        }
    }
}