using System;
using System.Collections.Generic;
using ProyectM2.Inventory;
using ProyectM2.UI.Sections;
using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.Personalization
{
    public abstract class SectionUI<T,TU> : MonoBehaviour where T : ItemData where TU : ItemCardUI
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject _itemCardPrefab;
        [SerializeField] private Transform _sectionContainer;
        [SerializeField] private GameObject _sectionGameObject;
        [SerializeField] protected ItemType _sectionType;
        public UnityEvent<ItemData> OnItemSelectedEvent;
        public event Action OnOpenMenu;
        protected List<ItemCardUI> _itemsUI = new List<ItemCardUI>();

        public bool IsVisible
        {
            get => _sectionContainer.gameObject.activeInHierarchy;
        }

        private void Awake()
        {
            SetAllItems();
        }

        protected abstract void SetAllItems();

        protected TU CreateNewItem(T item)
        {
            var itemGo = Instantiate(_itemCardPrefab, _sectionContainer);
            var itemUI = itemGo.GetComponent<TU>();
            _itemsUI.Add(itemUI);
            itemUI.SetItemData(item);
            itemUI.onItemSelected += OnItemSelected;
            return itemUI;
        }

        protected void OnDestroy()
        {
            foreach (var itemCardUI in _itemsUI)
            {
                itemCardUI.onItemSelected -= OnItemSelected;
            }
        }

        private void OnItemSelected(ItemData item)
        {
            OnItemSelectedEvent?.Invoke(item);
        }

        public virtual void Show()
        {
            if (IsVisible) return;
            _sectionGameObject.SetActive(true);
            OnOpenMenu?.Invoke();
        }

        public virtual void Hide()
        {
            _sectionGameObject.SetActive(false);
        }
    }
}