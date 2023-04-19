using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

namespace ProyectM2
{
    public class JsonSaveData : MonoBehaviour
    {
        string _path;

        private void Start()
        {
            _path = Application.persistentDataPath + "/savedData.json";

            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\", "/") + "/" + Application.productName;

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);


            if (File.Exists(_path))
                LoadGame();
            else
                SaveGame();
        }

        public void SaveGame()
        {
            var instanciaClase = new ValuesToSaveInJson(); //estamos creando instancia de la clase

            if (File.Exists(_path))
                instanciaClase = LoadGame();
            
            instanciaClase.totalCurrencyOfPlayer += GameManager._levelCurrency;

            string dataToSave = JsonUtility.ToJson(instanciaClase, true);

            byte[] bytesToEncode = Encoding.UTF8.GetBytes(dataToSave);
            string encodedText = Convert.ToBase64String(bytesToEncode);

            File.WriteAllText(_path, encodedText);
        }

        public ValuesToSaveInJson LoadGame()
        {
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