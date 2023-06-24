using ProyectM2.Gameplay;
using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Sound
{
    public class EngineSoundController : MonoBehaviour, IActivatable
    {
        [SerializeField] AudioClip _driving;
        [SerializeField] AudioClip _shootDamaging;
        [SerializeField] AudioClip _shootRetornable;
        [SerializeField] AudioSource _source;
        [SerializeField] private MoveController _moveController;
        private bool _isActive = false;

        private void Awake()
        {
            _source.Stop();
        }

        private void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
        }

        private void OnDisable()
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

        public void PlayShootingDamaging()
        {
            _source.PlayOneShot(_shootDamaging);
        }

        public void PlayShootingRetornable()
        {
            _source.PlayOneShot(_shootRetornable);
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
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
            StopSound();
        }
    }
}