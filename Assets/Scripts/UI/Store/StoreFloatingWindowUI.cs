﻿using System.Globalization;
using ProyectM2.Inventory;
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
        [SerializeField] private GameObject _equip;
        [SerializeField] private GameObject _buy;
        private ItemData _item;

        public void SetItemData(ItemData item)
        {
            _item = item;
            NameText = item.Name;
            PriceText = item.Price;
            ItemImage = item.Image;
            DescriptionText = item.Description;
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

        public ItemImage ItemImage
        {
            set
            {
                if (value.sprite == null)
                {
                    _itemImage.sprite = null;
                    _itemImage.color = value.color;
                }
                else
                {
                    _itemImage.sprite = value.sprite;
                    _itemImage.color = Color.white;
                }
            }
        }

        public void PurchaseItemUI()
        {
            EventManager.TriggerEvent("BuyItem", _item.UKey);
            _equip.SetActive(true);
            _buy.SetActive(false);
        }

    }
}