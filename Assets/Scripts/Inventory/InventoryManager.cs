using ProyectM2.Persistence;
using System.Collections.Generic;
using System.Linq;

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
            var itemBought = (IItem)obj[0];

            itemBought = ItemProvider.Instance.FindSpecificItem(itemBought.Name);

            var (instanciaClase, itemFoundedIndex) = LoadGameData(itemBought);

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
            var itemBought = (IItem)obj[0];

            itemBought = ItemProvider.Instance.FindSpecificItem(itemBought.Name);

            //var (instanciaClase, itemFoundedIndex) = LoadGameData(itemBought);
            var instanciaClase = LoadItemInInventory();

            //if (itemFoundedIndex != -1)
            //    instanciaClase.itemsInInventory[itemFoundedIndex].itemQuantity += 1;
            //else
            //    instanciaClase.itemsInInventory.Add(new Item(itemBought.Name, itemBought.Type, 1, false));

            DataPersistance.Instance.WriteJson(instanciaClase);
        }

        private void ActiveItemHandler(object[] obj)
        {
            var itemBought = (IItem)obj[0];

            itemBought = ItemProvider.Instance.FindSpecificItem(itemBought.Name);

            var (instanciaClase, itemFoundedIndex) = LoadGameData(itemBought);

            var itemsToDeactivate = instanciaClase.itemsInInventory.Where(item =>
                item.itemType == instanciaClase.itemsInInventory[itemFoundedIndex].itemType);


            foreach (var itemToDesactivate in itemsToDeactivate)
            {
                itemToDesactivate.isActive = false;
            }

            instanciaClase.itemsInInventory[itemFoundedIndex].isActive = true;

            DataPersistance.Instance.WriteJson(instanciaClase);
        }

        private (ValuesToSaveInJson, int) LoadGameData(IItem item)
        {
            //var itemFoundedIndex = instanciaClase.FindItemIndex(item.UKey);
            //return (instanciaClase, itemFoundedIndex);
            return (new ValuesToSaveInJson(), 2); //borrar wei

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