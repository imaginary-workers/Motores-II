using ProyectM2.Persistence;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProyectM2
{
    public class PowerUpsUI : MonoBehaviour
    {
        [SerializeField] private string _uniqueKeyFromPowerUp;
        [SerializeField] private TextMeshProUGUI _myText;
        private int _itemCount;
        private bool _canUsePowerUp;

        private void Start()
        {
            _canUsePowerUp = true;

            var items = DataPersistance.Instance.LoadGame();
            var itemsInInventory = items.itemsInInventory;
            var itemFoundedIndex = items.FindItemIndex(_uniqueKeyFromPowerUp);
            if (itemFoundedIndex != -1)
            {
                _itemCount = itemsInInventory[itemFoundedIndex].itemQuantity;
                _myText.text = _itemCount.ToString();
            }
            else
            {
                _myText.text = "0";
                _canUsePowerUp = false;
            }
        }

        public void SetActivationOfPowerUp()
        {
            if (_canUsePowerUp)
            {
                var item = ItemProvider.Instance.FindSpecificItem(_uniqueKeyFromPowerUp);
                EventManager.TriggerEvent("UseItem", item);
                _myText.color = Color.green;
                _myText.text = (_itemCount - 1).ToString();
                _canUsePowerUp = false;
            }
        }

    }
}
