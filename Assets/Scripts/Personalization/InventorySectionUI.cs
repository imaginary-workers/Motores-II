using System;
using System.Collections;
using System.Collections.Generic;
using ProyectM2.Inventory;
using ProyectM2.UI.Inventory;
using UnityEngine;

namespace ProyectM2.Personalization
{
    public class InventorySectionUI : SectionUI<ItemData, InventoryItemUI>
    {
        private void OnEnable()
        {
            SetAllItems();
        }

        protected override void SetAllItems()
        {
            var itemsUICounts = _itemsUI.Count;
            var allItems = InventoryManager.Instance.GetAllItems();
            allItems = allItems.FindAll(i => i.itemType == _sectionType);
            for (int i = 0; i < allItems.Count; i++)
            {
                var findSpecificItem = ItemProvider.Instance.FindSpecificItem(allItems[i].itemID);
                InventoryItemUI itemUI;
                if (itemsUICounts > 0)
                {
                    itemUI = _itemsUI[i];
                    itemUI.SetItemData(findSpecificItem);
                    itemUI.onItemSelected += OnItemSelected;
                    itemsUICounts--;
                }
                else
                {
                    itemUI = CreateNewItem(findSpecificItem);
                }
                itemUI.QuantityText = allItems[i].itemQuantity;
                
            }
        }
    }
}