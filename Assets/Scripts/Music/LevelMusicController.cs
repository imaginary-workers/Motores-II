using System.Collections;
using UnityEngine;

namespace ProyectM2.Music
{
    public class LevelMusicController: MonoBehaviour
    {
        [SerializeField] private AudioClip _initMusicLevelClip;
        private void OnEnable()
        {
            EventManager.StartListening("StartLevel", OnStartLevelHandler);
            EventManager.StartListening("GoToBonus", OnGoToBonesHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("StartLevel", OnStartLevelHandler);
            EventManager.StopListening("GoToBonus", OnGoToBonesHandler);
        }

        private void OnGoToBonesHandler(object[] obj)
        {
            PlayerPrefs.SetFloat("LevelMusicPlayedTime", MusicManager.Instance.GetPlayedTime());
        }

        private void OnStartLevelHandler(object[] obj)
        {
            var playedTime = PlayerPrefs.GetFloat("LevelMusicPlayedTime", 0);
            MusicManager.Instance.PlayMusic(_initMusicLevelClip, playedTime);
        }

        private void Awake()
        {
            MusicManager.Instance.PlayMusic(_initMusicLevelClip);
        }
    }
}