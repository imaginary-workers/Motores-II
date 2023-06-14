using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI.Store
{
    public class StoreItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _typeText;
        [SerializeField] private TextMeshProUGUI _price; 
        [SerializeField] private Image _itemImage;
        
        public void SetItemData(string nameText, float price, string type, Image itemImage)
        {
            NameText = nameText;
            PriceText = price;
            ItemImage = itemImage;
            TypeText = type;
        }

        public string NameText
        {
            get => _nameText.text;
            set => _nameText.text = value;
        }
        
        public string TypeText
        {
            get => _typeText.text;
            set => _typeText.text = value;
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
    }
}