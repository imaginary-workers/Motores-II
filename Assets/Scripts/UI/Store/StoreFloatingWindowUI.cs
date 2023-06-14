using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI.Store
{
    public class StoreFloatingWindowUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText; 
        [SerializeField] private TextMeshProUGUI _price; 
        [SerializeField] private TextMeshProUGUI _descriptionText; 
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _typeText;
        private IStoreItem _storeItem;

        public void SetItemData(IStoreItem storeItem)
        {
            _storeItem = storeItem;
            NameText = storeItem.Name;
            PriceText = storeItem.Price;
            ItemImage = storeItem.Image;
            TypeText = storeItem.Type;
            TypeText = storeItem.Type;
        }

        public string NameText
        {
            get => _nameText.text;
            set => _nameText.text = value;
        }
        
        public string DescriptionText
        {
            get => _descriptionText.text;
            set => _descriptionText.text = value;
        }
        
        public float PriceText
        {
            get => float.Parse(_price.text);
            set => _price.text = value.ToString(CultureInfo.InvariantCulture);
        }
        
        public Image ItemImage
        {
            get => _itemImage;
            set => _itemImage = value;
        }
        
        public string TypeText
        {
            get => _typeText.text;
            set => _typeText.text = value;
        }

        public void PurchaseItemUI()
        {
            EventManager.TriggerEvent("BuyItem", _storeItem);
        }
    }
}