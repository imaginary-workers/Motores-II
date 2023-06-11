using System;
using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Sound
{
    public class EngineSoundController  : MonoBehaviour
    {
        [SerializeField] AudioClip _driving;
        [SerializeField] AudioSource _source;
        [SerializeField] private MoveController _moveController;
        private bool _isGameOnPause = true;

        private void Awake()
        {
            _source.Stop();
        }

        protected virtual void OnEnable()
        {
            EventManager.StartListening("OnPause", PauseRunSound);
            EventManager.StartListening("OnPause", PauseRunSound);
        }
        
        protected virtual void OnDisable()
        {
            EventManager.StopListening("OnPause", PauseRunSound);
        }

        private void Update()
        {
            if (_isGameOnPause) return;
            if (_moveController.Speed <= 0)
            {
                StopSound();
            }
            else
            {
                RunSound();
            }
        }

        private void PauseRunSound(object[] obj)
        {
            if (obj.Length == 0) return;
            _isGameOnPause = (bool)obj[0];
            if (_isGameOnPause)
            {
                StopSound();
            }
            else
            {
                if (_source != null)
                {
                    RunSound();
                }
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
    }
}