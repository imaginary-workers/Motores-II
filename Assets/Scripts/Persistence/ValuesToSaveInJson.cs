using System;
using System.Collections.Generic;

namespace ProyectM2
{
    [Serializable]
    public class ValuesToSaveInJson
    {
        public int totalCurrencyOfPlayer;
        public int totalCurrencyGainOfPlayer;
        public int lastLevel;
        public float maxGas;
        public float timePlayed;
        public float musicVolume = .5f;
        public float soundVolume = .5f;
        public int stamina = -1;
        public string nextStaminaTime;
        public string lastStaminaTime;
        public List<Item> itemsInInventory;

        public int FindItemIndex(string itemName)
        {
            return itemsInInventory.FindIndex((itemAux) => itemAux.itemName == itemName);
        }
    }

    [Serializable]
    public class Item
    {
        public string itemName;
        public string itemType;
        public int itemQuantity;
        public bool isActive;

        public Item(string name, string type, int quantity, bool isActive)
        {
            this.itemName = name;
            this.itemType = type;
            this.itemQuantity = quantity;
            this.isActive = isActive;
        }
    }
}
