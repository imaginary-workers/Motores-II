using System.Globalization;
using ProyectM2.Inventory;
using ProyectM2.UI.Sections;
using TMPro;
using UnityEngine;

namespace ProyectM2.UI.Inventory
{
    public class InventoryItemUI : ItemCardUI
    {
        [SerializeField] private TextMeshProUGUI _quantity;
        public void SetItemData(ItemData item, int cantidad = 1)
        {
            SetItemData(item);
            QuantityText = item.Price;
        }

        public float QuantityText
        {
            get => int.Parse(_quantity.text);
            set => _quantity.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}