using ProyectM2.Gameplay;
using UnityEngine;

namespace ProyectM2.Music
{
    public class LevelMusicController: MonoBehaviour, IActivatable
    {
        [SerializeField] private AudioClip _initMusicLevelClip;
        private float _playedTimeLevelMusic = 0f;

        public void Activate()
        {
            MusicManager.Instance.PlayMusic(_initMusicLevelClip, _playedTimeLevelMusic);
        }

        public void Deactivate()
        {
            _playedTimeLevelMusic = MusicManager.Instance.GetPlayedTime();
            MusicManager.Instance.StopMusic();
        }
    }
}