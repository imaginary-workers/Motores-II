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

            var itemFoundedIndex = LoadGameData(itemBought.UKey);
            var instanciaClase = LoadItemInInventory();

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

            var itemFoundedIndex = LoadGameData(itemBought.UKey);
            var instanciaClase = LoadItemInInventory();

            if (itemFoundedIndex != -1)
                instanciaClase.itemsInInventory[itemFoundedIndex].itemQuantity += 1;
            else
                instanciaClase.itemsInInventory.Add(new Item(itemBought.Name, itemBought.Type, 1, false));

            DataPersistance.Instance.WriteJson(instanciaClase);
        }

        private void ActiveItemHandler(object[] obj)
        {
            var itemBought = ItemProvider.Instance.FindSpecificItem((string)obj[0]);

            var itemFoundedIndex = LoadGameData(itemBought.UKey);
            var instanciaClase = LoadItemInInventory();

            var itemsToDeactivate = instanciaClase.itemsInInventory.Where(item =>
                item.itemType == instanciaClase.itemsInInventory[itemFoundedIndex].itemType);


            foreach (var itemToDesactivate in itemsToDeactivate)
            {
                itemToDesactivate.isActive = false;
            }

            instanciaClase.itemsInInventory[itemFoundedIndex].isActive = true;

            DataPersistance.Instance.WriteJson(instanciaClase);
        }

        private int LoadGameData(string itemId)
        {
            return LoadItemInInventory().FindItemIndex(itemId);
        }

        private ValuesToSaveInJson LoadItemInInventory()
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
            
            var items = new List<Item>();
            foreach (var item in LoadItemInInventory().itemsInInventory)
            {
                items.Add(item);
            }
            return items;
        }
    }
}