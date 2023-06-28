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
                _myText.text = _itemCount.ToString();
                _canUsePowerUp = true;
            }
            else
            {
                _myText.text = _itemCount.ToString();
            }
        }

        private void OnDisable()
        {
            _myText.color = Color.white;
        }

        public void SetActivationOfPowerUp()
        {
            if (_canUsePowerUp)
            {
                var item = ItemProvider.Instance.FindSpecificItemByName(_myItemName);
                EventManager.TriggerEvent("UseItem", item.UKey);
                _myText.color = Color.green;
                _myText.text = (_itemCount - 1).ToString();
                _canUsePowerUp = false;
            }
        }

    }
}
