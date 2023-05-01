using System;
using UnityEngine;

namespace ProyectM2.Sound
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] AudioClip _driving;
        [SerializeField] AudioSource _source;

        private void Awake()
        {
            _source.Stop();
        }

        private void OnEnable()
        {
            EventManager.StartListening("OnPause", PauseRunSound);
        }

        private void PauseRunSound(object[] obj)
        {
            if (obj.Length == 0) return;
            var isPause = (bool)obj[0];
            Debug.Log(gameObject.name + " nombre de SoundManager");
            if (isPause)
            {
                _source.Stop();
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
            _source.Play();
        }
    }
}
