using UnityEngine;
using UnityEngine.UI;
using ProyectM2.Persistence;

namespace ProyectM2.Music
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider = null;
        [SerializeField] private Slider soundSlider = null;
        public static float musicVolume = 0.5f;
        public static float soundVolume = 0.5f;
        private ValuesToSaveInJson _myJsonData;

        private void Awake()
        {
            GetVolumeData();
        }

        private void OnEnable()
        {
            musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        }

        private void OnMusicSliderValueChanged(float value)
        {
            musicVolume = value;
            MusicManager.Instance.SetVolume(musicVolume, soundVolume);
        }

        private void OnSoundSliderValueChanged(float value)
        {
            soundVolume = value;
            MusicManager.Instance.SetVolume(musicVolume, soundVolume);
        }

        public void SaveVolume()
        {
            DataPersistance.Instance.UpdateVolume(musicVolume, soundVolume);
        }

        public void GetVolumeData()
        {
            _myJsonData = DataPersistance.Instance.LoadGame();
            musicVolume = _myJsonData.musicVolume;
            soundVolume = _myJsonData.soundVolume;
            MusicManager.Instance.SetVolume(musicVolume, soundVolume);
            UpdateSlider(musicVolume, soundVolume);
        }

        public void ResetVolume()
        {
            var middleMusicVolume = (musicSlider.maxValue + musicSlider.minValue) / 2;
            var middleSoundVolume = (soundSlider.maxValue + soundSlider.minValue) / 2;
            MusicManager.Instance.SetVolume(middleMusicVolume, middleSoundVolume);
            UpdateSlider(middleMusicVolume, middleSoundVolume);
        }

        public void UpdateSlider(float musicSliderValue, float soundSliderValue)
        {
            musicSlider.value = musicSliderValue;
            soundSlider.value = soundSliderValue;
        }
    }
}