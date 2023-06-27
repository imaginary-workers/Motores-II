using System;
using ProyectM2.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI.Sections
{
    public class ItemCardUI<T> : MonoBehaviour where T : ItemData
    {
        public event Action<T> onItemSelected;
        [SerializeField] protected TextMeshProUGUI _nameText;
        [SerializeField] protected Image _itemImage;
        protected T _item;
        public virtual void SetItemData(T item)
        {
            _item = item;
            NameText = item.Name;
            ItemImage = item.Image;
        }

        public string NameText
        {
            get => _nameText.text;
            set => _nameText.text = value;
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