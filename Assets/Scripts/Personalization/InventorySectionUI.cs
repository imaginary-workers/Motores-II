using ProyectM2.Inventory;
using ProyectM2.UI.Inventory;
using UnityEngine;

namespace ProyectM2.Personalization
{
    public class InventorySectionUI : SectionUI<ItemData, InventoryItemUI>
    {
        private int n = 1;
        protected override void SetAllItems()
        {
            var allItems = InventoryManager.Instance.GetAllItems();
            foreach (var item in allItems)
            {
                if (item.itemType != _sectionType) continue;
                if (_sectionType == ItemType.Chassis)
                {
                    Debug.Log(n++);
                }
                var inventoryItemUI = CreateNewItem(ItemProvider.Instance.FindSpecificItem(item.itemID));
                inventoryItemUI.QuantityText = item.itemQuantity;
            }
        }
    }
}