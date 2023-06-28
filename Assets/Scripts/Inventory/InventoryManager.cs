using ProyectM2.Persistence;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProyectM2.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private void OnEnable()
        {
            EventManager.StartListening("BuyItem", BuyItemHandler);
            EventManager.StartListening("UseItem", UseItemHandler);
            EventManager.StartListening("ActiveItem", ActiveItemHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("BuyItem", BuyItemHandler);
            EventManager.StopListening("UseItem", UseItemHandler);
            EventManager.StopListening("ActiveItem", ActiveItemHandler);
        }

        private void UseItemHandler(object[] obj)
        {

            var itemBought = ItemProvider.Instance.FindSpecificItem((string)obj[0]);

            var instanciaClase = LoadGameData();
            var itemFoundedIndex = instanciaClase.FindItemIndex(itemBought.UKey);

            if (itemFoundedIndex != -1)
            {
                var itemFounded = instanciaClase.itemsInInventory[itemFoundedIndex];
                itemFounded.itemQuantity -= 1;
                if (itemFounded.itemQuantity <= 0)
                    instanciaClase.itemsInInventory.Remove(itemFounded);
            }

            DataPersistance.Instance.WriteJson(instanciaClase);
        }

        private void BuyItemHandler(object[] obj)
        {
            var itemBought = ItemProvider.Instance.FindSpecificItem((string)obj[0]);

            var instanciaClase = LoadGameData();
            var itemFoundedIndex = instanciaClase.FindItemIndex(itemBought.UKey);

            if (itemFoundedIndex != -1)
                instanciaClase.itemsInInventory[itemFoundedIndex].itemQuantity += 1;
            else
                instanciaClase.itemsInInventory.Add(new Item(itemBought.UKey, itemBought.Type, 1, false));

            DataPersistance.Instance.WriteJson(instanciaClase);
        }

        private void ActiveItemHandler(object[] obj)
        {
            var itemBought = ItemProvider.Instance.FindSpecificItem((string)obj[0]);

            var instanciaClase = LoadGameData();
            var itemFoundedIndex = instanciaClase.FindItemIndex(itemBought.UKey);

            var itemsToDeactivate = instanciaClase.itemsInInventory.Where(item =>
                item.itemType == instanciaClase.itemsInInventory[itemFoundedIndex].itemType);


            foreach (var itemToDesactivate in itemsToDeactivate)
            {
                itemToDesactivate.isActive = false;
            }

            instanciaClase.itemsInInventory[itemFoundedIndex].isActive = true;

            DataPersistance.Instance.WriteJson(instanciaClase);
        }

        private ValuesToSaveInJson LoadGameData()
        {
            var instanciaClase = DataPersistance.Instance.LoadGame();
            if (instanciaClase.itemsInInventory == null)
            {
                instanciaClase.itemsInInventory = new List<Item>();
            }
            return instanciaClase;
        }

        public List<Item> GetAllItems()
        {
            return new List<Item>(LoadGameData().itemsInInventory);
        }

        public Item FindItemInInventory(string itemId)
        {
            var gameData = LoadGameData();
            var itemIndex = gameData.FindItemIndex(itemId);
            if (itemIndex != -1)
                return gameData.itemsInInventory[itemIndex];
            return new NullItem();
        }
    }
}