﻿using System;
using System.Collections.Generic;
using ProyectM2.Inventory;
using ProyectM2.UI.Store;
using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.Personalization
{
    public abstract class SectionUI: MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject _itemCardPrefab;
        [SerializeField] private StoreFloatingWindowUI _itemFloatingWindowUI;
        [SerializeField] private Transform _sectionContainer;
        [SerializeField] protected ItemType _sectionType;
        public UnityEvent OnItemSelectedEvent;
        public event Action OnOpenMenu;
        private List<StoreItemUI> _itemsUI = new List<StoreItemUI>();
        public bool IsVisible
        {
            get => _sectionContainer.gameObject.activeInHierarchy;
        }

        private void Awake()
        {
            SetAllItems();
        }

        protected abstract void SetAllItems();

        protected void CreateNewItem(StoreItem item)
        {
            var itemGo = Instantiate(_itemCardPrefab, _sectionContainer);
            var itemUI = itemGo.GetComponent<StoreItemUI>();
            _itemsUI.Add(itemUI);
            itemUI.SetItemData(item);
            itemUI.onItemSelected += OnItemSelected;
        }

        protected virtual void OnItemSelected(StoreItem storeItem)
        {
            _itemFloatingWindowUI.SetItemData(storeItem);
            OnItemSelectedEvent?.Invoke();
        }

        protected virtual void OnDestroy()
        {
            foreach (var itemUI in _itemsUI)
            {
                itemUI.onItemSelected -= OnItemSelected;
            }
        }

        public virtual void Show()
        {
            if (IsVisible) return;
            _sectionContainer.gameObject.SetActive(true);
            OnOpenMenu?.Invoke();
        }
        
        public virtual void Hide()
        {
            _sectionContainer.gameObject.SetActive(false);
        }
    }
}