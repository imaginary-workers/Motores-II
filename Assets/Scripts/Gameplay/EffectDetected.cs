using ProyectM2.Gameplay;
using UnityEngine;

namespace ProyectM2
{
    public class EffectDetected : MonoBehaviour, IActivatable
    {
        [SerializeField] ParticleSystem _particle;
        [SerializeField] PlayerFiresBackController _playerFiresBackController;

        private bool _isPlaying;
        private float _particleCountDown = 0f;
        private bool _isActive = true;

        private void OnEnable()
        {
            _playerFiresBackController.OnFireBack += PlayOn;
            ScreenManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            _playerFiresBackController.OnFireBack -= PlayOn;
            ScreenManager.Instance.Unsubscribe(this);
        }

        private void Update()
        {
            if (_particle.isPlaying)
            {
                _particleCountDown -= Time.deltaTime;
                if (_particleCountDown <= 0)
                {
                    _particle.Stop();
                }
            }
        }

        public void PlayOn(Vector3 position)
        {
            if (!_isActive) return;
            if (_particle.isPlaying)
            {
                _particle.Stop();
            }

            _particle.transform.position = position;
            _particle.Play();
            _particleCountDown = _particle.main.duration;
        }

        public void Activate()
        {
            _isActive = true;
            if (_particle.isPaused)
                _particle.Play();
        }

        public void Deactivate()
        {
            _isActive = false;
            if (_particle.isPlaying)
                _particle.Pause();
        }
    }
}
