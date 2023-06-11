using UnityEngine;
using System.IO;
using System;
using System.Text;
using ProyectM2.Gameplay;

namespace ProyectM2.Persistence
{
    public class DataPersistance : Singleton<DataPersistance>
    {
        string _path;

        private void Awake()
        {
            _path = Application.persistentDataPath + "/savedData.json";

            if (File.Exists(_path))
                LoadGame();

            SaveGame();
        }

        public void UpdateVolume(float musicVolume, float soundVolume)
        {
            var instanciaClase = LoadGame();
            instanciaClase.musicVolume = musicVolume;
            instanciaClase.soundVolume = soundVolume;
            WriteJson(instanciaClase);

        }

        public void SaveGame()
        {
            var instanciaClase = new ValuesToSaveInJson();
            if (File.Exists(_path))
                instanciaClase = LoadGame();

            instanciaClase.totalCurrencyOfPlayer += GameManager.levelCurrency;
            instanciaClase.totalCurrencyGainOfPlayer += GameManager.levelCurrency;
            instanciaClase.timePlayed = TimePlayed.Instance.TotalTimePlayed;
            GameManager.levelCurrency = 0;

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

            SaveGame();
        }
    }
}
