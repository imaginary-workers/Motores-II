using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProyectM2.Music;
using ProyectM2.Persistence;
using System;

namespace ProyectM2
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider = null;
        [SerializeField] private Slider soundSlider = null;
        public static float musicVolume = 0.5f;
        public static float soundVolume = 0.5f;

        ValuesToSaveInJson _myJsonData;


        private void Awake()
        {
            GetVolumeData();
        }

        private void OnEnable()
        {
            musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            musicSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        }

        private void OnMusicSliderValueChanged(float value)
        {
            musicVolume = value;
            MusicManager.Instance.SetVolume(musicVolume, soundVolume);
        }

        private void OnSoundSliderValueChanged(float value)
        {
            soundVolume = soundSlider.value;
            MusicManager.Instance.SetVolume(musicVolume, soundVolume);
        }

        public void SaveVolume()
        {
            DataPersistance.Instance.UpdateVolume(musicVolume, soundVolume);
        }

        public void GetVolumeData()
        {
            _myJsonData = DataPersistance.Instance.LoadGame();
            Debug.Log("GET music " + _myJsonData.musicVolume + " sound " + _myJsonData.soundVolume);
            musicSlider.value = _myJsonData.musicVolume;
            soundSlider.value = _myJsonData.soundVolume;
            MusicManager.Instance.SetVolume(_myJsonData.musicVolume, _myJsonData.soundVolume);
        }
    }
}
