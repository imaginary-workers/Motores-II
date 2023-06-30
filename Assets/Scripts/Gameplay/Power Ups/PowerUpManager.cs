using ProyectM2.Inventory;
using ProyectM2.SO;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.Gameplay.PowerUps
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private GameObject _shield;
        [SerializeField] private string _shieldPowerUpName;
        [SerializeField] private string _currencyMultiplyPowerUpName;
        [SerializeField] DataIntObservable _currencyValue;
        private List<Item> _itemsInInventory;

        private void Start()
        {
            _itemsInInventory = InventoryManager.Instance.GetAllItems().FindAll((i)=>i.itemType == ItemType.PowerUp);

            foreach (var item in _itemsInInventory)
            {
                var itemData = ItemProvider.Instance.FindSpecificItem(item.itemID);

                if (!item.isActive)
                {
                    if (itemData.Name == _currencyMultiplyPowerUpName)
                        _currencyValue.value = 1;
                }
                else
                {
                    if (itemData.Name == _shieldPowerUpName)
                        _shield.SetActive(true);

                    if (itemData.Name == _currencyMultiplyPowerUpName)
                        _currencyValue.value = 2;
                }

            }
        }

        
    }
}
