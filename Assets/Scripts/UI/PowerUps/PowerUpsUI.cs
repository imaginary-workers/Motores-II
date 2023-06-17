using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProyectM2
{
    public class PowerUpsUI : MonoBehaviour
    {
        [SerializeField] private string _nameOfItemPowerUp;
        [SerializeField] private TextMeshProUGUI _myText;
        private int _itemCount;
        private bool _canUsePowerUp;

        private void Start()
        {
            _canUsePowerUp = true;
            var itemsInInventory = ItemProvider.FindSavedItems();
            if (itemsInInventory.TryGetValue(_nameOfItemPowerUp, out int itemCount))
            {
                _itemCount = itemCount;
                _myText.text = _itemCount.ToString();
            }
            else
                _myText.text = "0";
        }

        private void SetActivationOfPowerUp()
        {
            if (_canUsePowerUp)
            {
                _myText.color = Color.green;
                _myText.text = (_itemCount - 1).ToString();
                _canUsePowerUp = false;
            }
        }

    }
}
