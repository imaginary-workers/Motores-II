using System;
using System.Collections.Generic;
using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    public class StoreControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _sectionPrefab;
        [SerializeField] private GameObject _itemCardPrefab;
        [SerializeField] private GameObject _sectionParent;
        private List<IStoreItem> _allItems;
        private List<StoreSectionUI> _sectionsUI = new List<StoreSectionUI>();

        private void Awake()
        {
            _allItems = StoreListenerHandler.AllItems;
            foreach (var item in _allItems)
            {
                if (_sectionsUI.Count == 0 || !_sectionsUI.Find((section) => section.SectionNameText.Equals(item.Type)))
                {
                    var newSection = Instantiate(_sectionPrefab, _sectionParent.transform);
                    var sectionUI = newSection.GetComponent<StoreSectionUI>();
                    sectionUI.SectionNameText = item.Type;
                    var itemGO = Instantiate(_itemCardPrefab);
                    var storeItemUI = itemGO.GetComponent<StoreItemUI>();
                    storeItemUI.SetItemData(item);
                    sectionUI.AddItem(itemGO);
                }
            }
        }
    }
}