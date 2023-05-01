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
        }
        private void OnDisable()
        {
            EventManager.StopListening("StartLevel", OnStartLevelHandler);
        }
        private void OnStartLevelHandler(object[] obj)
        {
            MusicManager.Instance.PlayMusic(_initMusicLevelClip);
        }

        private void Awake()
        {
            MusicManager.Instance.PlayMusic(_initMusicLevelClip);
        }
    }
}