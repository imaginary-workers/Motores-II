using System;
using UnityEngine;

namespace ProyectM2.Music
{
    public class MusicManager : Singleton<MusicManager>
    {
        private AudioSource _audioSource;
        [SerializeField] private GameObject audioSourcePrefab;
        private void Start()
        {
            _audioSource = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
        }

        public float GetPlayedTime()
        {
            return _audioSource.time;
        }

        public void LoadMusic()
        {   
        }

        public void PlayMusic(AudioClip clip, float from = 0f)
        {
            _audioSource.time = from;
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void StopMusic()
        {
            _audioSource.Stop();
        }

        public void SetVolume()
        {
        }

        public void PauseMusic()
        {
            _audioSource.Pause();
        }

        public void ResumeMusic()
        {
            _audioSource.Play();
        }

        public void SwitchMusic()
        {
        }
    }
}