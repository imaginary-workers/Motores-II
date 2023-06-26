using System;
using System.Collections.Generic;
using ProyectM2.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.UI.Store
{
    public class StoreSectionUI: MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject _itemCardPrefab;
        [SerializeField] private StoreFloatingWindowUI _itemFloatingWindowUI;
        [SerializeField] private Transform _sectionContainer;
        public UnityEvent OnItemSelectedEvent;
        public event Action OnOpenMenu;
        private List<StoreItemUI> _itemsUI = new List<StoreItemUI>();
        public bool IsVisible
        {
            get => _sectionContainer.gameObject.activeInHierarchy;
        }

        private void Awake()
        {
            var allItems = ItemProvider.AllItems;
            foreach (var item in allItems)
            {
                if (item.Type != ItemType.Chassis) continue;

                var itemGo = Instantiate(_itemCardPrefab, _sectionContainer);
                var itemUI = itemGo.GetComponent<StoreItemUI>();
                _itemsUI.Add(itemUI);
                itemUI.SetItemData(item);
                itemUI.onItemSelected += OnItemSelected;
            }
        }
        
        protected virtual void OnItemSelected(IStoreItem storeItem)
        {
            _itemFloatingWindowUI.SetItemData(storeItem);
            OnItemSelectedEvent?.Invoke();
        }

        private void OnDestroy()
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