using System.Globalization;
using ProyectM2.Inventory;
using ProyectM2.UI.Sections;
using TMPro;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    public class StoreItemUI : ItemCardUI<StoreItem>
    {
        [SerializeField] private TextMeshProUGUI _price; 
        public override void SetItemData(StoreItem item)
        {
            base.SetItemData(item);
            PriceText = item.Price;
        }

        public float PriceText
        {
            get => float.Parse(_price.text);
            set => _price.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}