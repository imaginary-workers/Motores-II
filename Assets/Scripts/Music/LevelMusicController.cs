using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2.Music
{
    public class LevelMusicController: MonoBehaviour
    {
        [SerializeField] private AudioClip _initMusicLevelClip;
        private bool _isBonusLevel;

        private void Awake()
        {
            var isBonusLevel = SessionGameData.GetData("IsInBonusLevel");
            _isBonusLevel = isBonusLevel != null && (bool) isBonusLevel;
        }

        private void OnEnable()
        {
            EventManager.StartListening("OnPause", OnStartLevelHandler);
            if (_isBonusLevel) return;
            EventManager.StartListening("TeleportToBonusLevel", OnGoToBonesHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnPause", OnStartLevelHandler);
            if (_isBonusLevel) return;
            EventManager.StopListening("TeleportToBonusLevel", OnGoToBonesHandler);
        }

        private void OnGoToBonesHandler(object[] obj)
        {
            //PlayerPrefs.SetFloat("LevelMusicPlayedTime", MusicManager.Instance.GetPlayedTime());
            SessionGameData.SaveData("LevelMusicPlayedTime", MusicManager.Instance.GetPlayedTime());
        }

        private void OnStartLevelHandler(object[] obj)
        {
            if (obj.Length <= 0) return;
            if ((bool) obj[0]) return;

            var playedTime = 0f;
            if (!_isBonusLevel)
            {
                //playedTime = PlayerPrefs.GetFloat("LevelMusicPlayedTime", 0);
                if (SessionGameData.GetData("LevelMusicPlayedTime") == null)
                    playedTime = 0;
                else
                    playedTime = (float)SessionGameData.GetData("LevelMusicPlayedTime");
            }

            MusicManager.Instance.PlayMusic(_initMusicLevelClip, playedTime);
        }
    }
}