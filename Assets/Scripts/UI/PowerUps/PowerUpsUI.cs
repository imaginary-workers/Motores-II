using ProyectM2.Inventory;
using ProyectM2.Persistence;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProyectM2
{
    public class PowerUpsUI : MonoBehaviour
    {
        [SerializeField] private string _myItemName;
        [SerializeField] private TextMeshProUGUI _myText;
        private int _itemCount = 0;
        private bool _canUsePowerUp;

        private void OnEnable()
        {
            _canUsePowerUp = false;

            var item = ItemProvider.Instance.FindSpecificItemByName(_myItemName);

            var itemInInventory = InventoryManager.Instance.FindItemInInventory(item.UKey);

            if (itemInInventory.itemType != ItemType.NULL)
            {
                _itemCount = itemInInventory.itemQuantity;
                if (itemInInventory.isActive)
                {
                    UpdateUI(_itemCount, Color.green);
                }
                else
                {
                    _canUsePowerUp = true;
                    UpdateUI(_itemCount, Color.black);
                }
            }
            else
            {
                UpdateUI(_itemCount, Color.black);
            }
        }

        private void OnDisable()
        {
            _myText.color = Color.black;
        }

        public void SetActivationOfPowerUp()
        {
            if (_canUsePowerUp)
            {
                var item = ItemProvider.Instance.FindSpecificItemByName(_myItemName);
                EventManager.TriggerEvent("ActiveItem", item.UKey);
                SetActivationOfPowerUp();
            }
        }

        public void UpdateUI(int quantity, Color color)
        {
                _myText.color = color;
                _myText.text = quantity.ToString();
                _canUsePowerUp = false;
        }

    }
}
