using UnityEngine;

namespace ProyectM2.Sound
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] AudioClip _driving;
        [SerializeField] AudioSource _source;

        public void RunSound()
        {
            if (_source.isPlaying && _source.clip == _driving) return;
            _source.clip = _driving;
            _source.Play();
        }
    }
}
