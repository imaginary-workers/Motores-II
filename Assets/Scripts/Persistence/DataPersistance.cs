using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using ProyectM2.SO;

namespace ProyectM2.Persistence
{
    public class DataPersistance : Singleton<DataPersistance>
    {
        [SerializeField] private DataIntObservable _levelCurrency;
        private string _path;

        protected override void Awake()
        {
            base.Awake();
            _path = Application.persistentDataPath + "/savedData.json";

            if (File.Exists(_path))
                LoadGame();

            UpdateCurrency();
        }

        public void UpdateVolume(float musicVolume, float soundVolume)
        {
            var instanciaClase = LoadGame();
            instanciaClase.musicVolume = musicVolume;
            instanciaClase.soundVolume = soundVolume;
            WriteJson(instanciaClase);

        }

        public void UpdateTime(float timePlayed)
        {
            var instanciaClase = LoadGame();

            instanciaClase.timePlayed = timePlayed;
            WriteJson(instanciaClase);
        }

        public void UpdateStaminaData(int stamina, string nextStaminaTime, string lastStaminaTime)
        {
            var instanciaClase = LoadGame();
            instanciaClase.stamina = stamina;
            instanciaClase.nextStaminaTime = nextStaminaTime;
            instanciaClase.lastStaminaTime = lastStaminaTime;
            WriteJson(instanciaClase);
        }

        public void UpdateCurrency()
        {
            var instanciaClase = LoadGame();
            instanciaClase.totalCurrencyOfPlayer += _levelCurrency.value;
            instanciaClase.totalCurrencyGainOfPlayer += _levelCurrency.value;
            _levelCurrency.value = 0;

            WriteJson(instanciaClase);
        }

        public void WriteJson(ValuesToSaveInJson data)
        {
            string dataToSave = JsonUtility.ToJson(data, true);
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(dataToSave);
            string encodedText = Convert.ToBase64String(bytesToEncode);

            File.WriteAllText(_path, encodedText);
        }

        public ValuesToSaveInJson LoadGame()
        {

            var instanciaClase = new ValuesToSaveInJson();

            if (File.Exists(_path))
            {
                string dataToLoad = File.ReadAllText(_path);
                byte[] bytesToDecode = Convert.FromBase64String(dataToLoad);
                string decodedText = Encoding.UTF8.GetString(bytesToDecode);
                JsonUtility.FromJsonOverwrite(decodedText, instanciaClase);
            }
            return instanciaClase;
        }

        public void DeleteData()
        {
            if (File.Exists(_path))
                File.Delete(_path);

            UpdateCurrency();
        }
#if UNITY_EDITOR
        [ContextMenu("Borra Todo")]
        public void DeleteDataTest()
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }
#endif
    }
}
