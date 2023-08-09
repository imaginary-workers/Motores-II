using ProyectM2.Inventory;
using ProyectM2.SO;
using System.Collections.Generic;
using ProyectM2.Persistence;
using UnityEngine;
using ProyectM2.Store;

namespace ProyectM2.Gameplay.PowerUps
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private GameObject _shield;
        [SerializeField] DataIntObservable _currencyValue;
        public static string shieldPowerUpName = "Escudo";
        public static string currencyMultiplyPowerUpName = "Mutiplicador de Monedas";
        private List<Item> _itemsInInventory;

        private void Start()
        {
            _itemsInInventory = InventoryManager.Instance.GetAllItems().FindAll((i)=>i.itemType == ItemType.PowerUp);

            foreach (var item in _itemsInInventory)
            {
                var itemData = ItemProvider.Instance.FindSpecificItem(item.itemID);

                if (!item.isActive)
                {
                    if (itemData.Name == currencyMultiplyPowerUpName)
                        _currencyValue.value = 1;
                }
                else
                {
                    if (itemData.Name == shieldPowerUpName)
                        _shield.SetActive(true);

                    if (itemData.Name == currencyMultiplyPowerUpName)
                        _currencyValue.value = 2;
                }
            }
        } 
    }
}
