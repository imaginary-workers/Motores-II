using UnityEngine;
using System.IO;
using System;
using System.Text;
using ProyectM2.Gameplay;
using System.Collections.Generic;

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

            UpdateCurrency();
        }

        public void UpdateVolume(float musicVolume, float soundVolume)
        {
            var instanciaClase = LoadGame();
            instanciaClase.musicVolume = musicVolume;
            instanciaClase.soundVolume = soundVolume;
            WriteJson(instanciaClase);

        }

        public void UpdateStoreData(IStoreItem item, string updateType)
        {
            var instanciaClase = LoadGame();

            if (instanciaClase.itemsInInventory == null)
            {
                instanciaClase.itemsInInventory = new List<Item>();
            }

            var itemFoundedIndex = instanciaClase.FindItemIndex(item.Name);
            if (updateType == "Buy")
            {
                if (itemFoundedIndex !=  -1)
                    instanciaClase.itemsInInventory[itemFoundedIndex].itemQuantity += 1;
                else
                    instanciaClase.itemsInInventory.Add(new Item(item.Name, 1));

                instanciaClase.totalCurrencyOfPlayer -= (int)item.Price;
            }
            else if (updateType == "Used")
            {
                if (itemFoundedIndex != -1)
                {
                    var itemFounded = instanciaClase.itemsInInventory[itemFoundedIndex];
                    itemFounded.itemQuantity -= 1;
                    if (itemFounded.itemQuantity <= 0)
                        instanciaClase.itemsInInventory.Remove(itemFounded);
                }
                else
                    Debug.LogError("Estas usando algo q no tenes");
            }

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
            var instanciaClase = new ValuesToSaveInJson();
            if (File.Exists(_path))
                instanciaClase = LoadGame();

            instanciaClase.totalCurrencyOfPlayer += GameManager.levelCurrency;
            instanciaClase.totalCurrencyGainOfPlayer += GameManager.levelCurrency;
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

            UpdateCurrency();
        }
#if UNITY_EDITOR
        [ContextMenu("Borra Todo")]
        public void DeleteDataTest()
        {
            Debug.Log(_path);
            if (File.Exists(_path))
                File.Delete(_path);
        }
#endif
    }
}
