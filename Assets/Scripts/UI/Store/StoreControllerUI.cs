using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.UI.Store
{
    public class StoreControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _sectionPrefab;
        [SerializeField] private GameObject _itemCardPrefab;
        [SerializeField] private Transform _sectionParent;
        [SerializeField] private StoreFloatingWindowUI _itemFloatingWindowUI;
        private List<IStoreItem> _allItems;
        private List<StoreSectionUI> _sectionsUI = new List<StoreSectionUI>();
        private List<StoreItemUI> _itemsUI = new List<StoreItemUI>();
        public UnityEvent OnItemSelectedEvent;

        private void Awake()
        {
            _allItems = ItemProvider.AllItems;
            foreach (var item in _allItems)
            {
                var sectionUI = _sectionsUI.Find((section) => section.SectionNameText.Equals(item.Type));
                if (_sectionsUI.Count == 0 || sectionUI == null)
                {
                    var newSection = Instantiate(_sectionPrefab, _sectionParent.transform);
                    sectionUI = newSection.GetComponent<StoreSectionUI>();
                    sectionUI.SectionNameText = item.Type;
                }
                var itemGo = Instantiate(_itemCardPrefab, sectionUI.Container);
                var itemUI = itemGo.GetComponent<StoreItemUI>();
                _itemsUI.Add(itemUI);
                itemUI.SetItemData(item);
                itemUI.onItemSelected += OnItemSelected;
            }
        }

        private void OnDestroy()
        {
            foreach (var itemUI in _itemsUI)
            {
                itemUI.onItemSelected -= OnItemSelected;
            }
        }

        private void OnItemSelected(IStoreItem storeItem)
        {
            _itemFloatingWindowUI.SetItemData(storeItem);
            OnItemSelectedEvent?.Invoke();
        }

    }
}