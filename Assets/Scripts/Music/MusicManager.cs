using UnityEngine;

namespace ProyectM2.Music
{
    public class MusicManager : Singleton<MusicManager>
    {
        private AudioSource _audioSource;
        private AudioSource _soundsAudioSource;
        [SerializeField] private GameObject audioSourcePrefab;
        [SerializeField] private GameObject soundAudioSourcePrefab;

        private void Start()
        {
            _audioSource = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
            _soundsAudioSource = soundAudioSourcePrefab.GetComponent<AudioSource>();
        }

        public float GetPlayedTime()
        {
            return _audioSource.time;
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

        public void SetVolume(float musicVolume, float soundVolume)
        {
            _audioSource.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", musicVolume);
            _soundsAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("SfxVolume", soundVolume);
        }

    }
}