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
        [SerializeField, Tooltip("Este es donde se mostraran los items")] private Transform _sectionContainer;
        [SerializeField, Tooltip("Este es el panel general de la seccion")] private Transform _sectionPanel;
        [SerializeField] private ItemType _sectionType;
        public UnityEvent OnItemSelectedEvent;
        public event Action OnOpenMenu;
        private List<StoreItemUI> _itemsUI = new List<StoreItemUI>();
        public bool IsVisible
        {
            get => _sectionPanel.gameObject.activeInHierarchy;
        }

        private void Awake()
        {
            var allItems = ItemProvider.Instance.AllItems;
            foreach (var item in allItems)
            {
                if (item.Type != _sectionType) continue;

                var itemGo = Instantiate(_itemCardPrefab, _sectionContainer);
                var itemUI = itemGo.GetComponent<StoreItemUI>();
                _itemsUI.Add(itemUI);
                itemUI.SetItemData(item);
                itemUI.onItemSelected += OnItemSelected;
            }
        }
        
        protected virtual void OnItemSelected(StoreItem item)
        {
            _itemFloatingWindowUI.SetItemData(item);
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
            _sectionPanel.gameObject.SetActive(true);
            OnOpenMenu?.Invoke();
        }
        
        public virtual void Hide()
        {
            _sectionPanel.gameObject.SetActive(false);
        }
    }
}