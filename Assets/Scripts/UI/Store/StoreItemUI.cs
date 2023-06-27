using System;
using System.Globalization;
using ProyectM2.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI.Store
{
    public class StoreItemUI : MonoBehaviour
    {
        public event Action<StoreItem> onItemSelected;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _price; 
        [SerializeField] private Image _itemImage;
        private StoreItem _item;

        public void SetItemData(StoreItem item)
        {
            _item = item;
            NameText = item.Name;
            PriceText = item.Price;
            ItemImage = item.Image;
        }

        public string NameText
        {
            get => _nameText.text;
            set => _nameText.text = value;
        }
        
        public float PriceText
        {
            get => float.Parse(_price.text);
            set => _price.text = value.ToString(CultureInfo.InvariantCulture);
        }
        
        public ItemImage ItemImage
        {
            set
            {
                if (value.sprite == null)
                {
                    _itemImage.color = value.color;
                }
                else
                {
                    _itemImage.sprite = value.sprite;
                }
            }
        }

        public void OnItemSelected()
        {
            onItemSelected?.Invoke(_item);
        }

        private void OnDestroy()
        {
            onItemSelected = null;
        }
    }
}