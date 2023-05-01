using UnityEngine;

namespace ProyectM2.Sound
{
    public class UISoundManager : Singleton<UISoundManager>
    {
        private AudioSource _audioSource;
        [SerializeField] private GameObject audioSourcePrefab;
        private void Start()
        {
            _audioSource = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        public void StopMusic()
        {
            _audioSource.Stop();
        }
    }
}
