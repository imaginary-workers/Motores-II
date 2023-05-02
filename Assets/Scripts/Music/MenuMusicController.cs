using UnityEngine;

namespace ProyectM2.Music
{
    public class MenuMusicController : MonoBehaviour
    {
        [SerializeField] private AudioClip _initMusicLevelClip;
        private void Awake()
        {
            MusicManager.Instance.PlayMusic(_initMusicLevelClip);
        }
    }
}