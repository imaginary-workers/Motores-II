using System.Collections.Generic;
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
                if (item.Type.ToLower() != "chassis") continue;

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

        public void Show()
        {
            _sectionContainer.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            _sectionContainer.gameObject.SetActive(false);
        }
    }
}