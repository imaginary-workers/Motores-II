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
        public float musicVolume;
        public float soundVolume;
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
        public int itemQuantity;

        public Item(string name, int quantity)
        {
            this.itemName = name;
            this.itemQuantity = quantity;
        }
    }
}
