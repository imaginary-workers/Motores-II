using System;
using System.Collections.Generic;
using ProyectM2.Inventory;

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

        public int FindItemIndex(string itemID)
        {
            return itemsInInventory.FindIndex((itemAux) => itemAux.itemID == itemID);
        }
    }

    [Serializable]
    public class Item
    {
        public string itemID;
        public ItemType itemType;
        public int itemQuantity;
        public bool isActive;

        public Item(string name, ItemType type, int quantity, bool isActive)
        {
            this.itemID = name;
            this.itemType = type;
            this.itemQuantity = quantity;
            this.isActive = isActive;
        }
    }
}
