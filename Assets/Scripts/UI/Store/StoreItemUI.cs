using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI.Store
{
    public class StoreItemUI : MonoBehaviour
    {
        public event Action<IStoreItem> onItemSelected;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _price; 
        [SerializeField] private Image _itemImage;
        private IStoreItem _storeItem;

        public void SetItemData(IStoreItem storeItem)
        {
            _storeItem = storeItem;
            NameText = storeItem.Name;
            PriceText = storeItem.Price;
            ItemImage = storeItem.Image;
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
        
        public Sprite ItemImage
        {
            get => _itemImage.sprite;
            set => _itemImage.sprite = value;
        }

        //Llamar desde el componente Button onclick
        public void OnItemSelected()
        {
            onItemSelected?.Invoke(_storeItem);
        }

        private void OnDestroy()
        {
            onItemSelected = null;
        }
    }
}