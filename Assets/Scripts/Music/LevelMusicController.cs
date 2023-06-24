using ProyectM2.Gameplay;
using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2.Music
{
    public class LevelMusicController: MonoBehaviour, IActivatable
    {
        [SerializeField] private AudioClip _initMusicLevelClip;
        private bool _isBonusLevel;
        private float _playedTimeLevelMusic = 0f;

        private void Awake()
        {
            var isBonusLevel = SessionGameData.GetData("IsInBonusLevel");
            _isBonusLevel = isBonusLevel != null && (bool) isBonusLevel;
        }

        private void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
            if (_isBonusLevel) return;
            // EventManager.StartListening("TeleportToBonusLevel", OnGoToBonesHandler);
        }

        private void OnDisable()
        {
            ScreenManager.Instance.Unsubscribe(this);
            if (_isBonusLevel) return;
            // EventManager.StopListening("TeleportToBonusLevel", OnGoToBonesHandler);
        }
        //
        // private void OnGoToBonesHandler(object[] obj)
        // {
        //     SessionGameData.SaveData("LevelMusicPlayedTime", MusicManager.Instance.GetPlayedTime());
        // }

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