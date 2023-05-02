using UnityEngine;
using System.IO;
using System;
using System.Text;
using ProyectM2.Gameplay;

namespace ProyectM2.Persistence
{
    public class DataPersistance : MonoBehaviour
    {
        string _path;

        private void Awake()
        {
            CheckPath();


            if (File.Exists(_path))
                LoadGame();

            SaveGame();
        }

        private void CheckPath()
        {
            _path = Application.persistentDataPath + "/savedData.json";

            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\", "/") + "/" + Application.productName;

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        public void SaveGame()
        {
            CheckPath();
            var instanciaClase = new ValuesToSaveInJson(); //estamos creando instancia de la clase
            if (File.Exists(_path))
                instanciaClase = LoadGame();

            instanciaClase.totalCurrencyOfPlayer += GameManager.levelCurrency;
            instanciaClase.totalCurrencyGainOfPlayer += GameManager.levelCurrency;
            instanciaClase.timePlayed += Time.realtimeSinceStartup;
            GameManager.levelCurrency = 0;

            string dataToSave = JsonUtility.ToJson(instanciaClase, true);
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(dataToSave);
            string encodedText = Convert.ToBase64String(bytesToEncode);

            File.WriteAllText(_path, encodedText);
        }

        public ValuesToSaveInJson LoadGame()
        {
            CheckPath();

            var instanciaClase = new ValuesToSaveInJson(); //estamos creando instancia de la clase

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
