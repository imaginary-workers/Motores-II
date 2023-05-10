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

        protected virtual void OnEnable()
        {
            EventManager.StartListening("OnPause", PauseRunSound);
        }
        
        protected virtual void OnDisable()
        {
            EventManager.StopListening("OnPause", PauseRunSound);
        }

        private void PauseRunSound(object[] obj)
        {
            if (obj.Length == 0) return;
            var isPause = (bool)obj[0];
            if (isPause)
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
    }
}
