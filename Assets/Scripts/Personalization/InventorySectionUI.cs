using ProyectM2.Inventory;
using ProyectM2.UI.Inventory;

namespace ProyectM2.Personalization
{
    public class InventorySectionUI : SectionUI<ItemData, InventoryItemUI>
    {
        protected override void SetAllItems()
        {
            var allItems = InventoryManager.Instance.GetAllItems();
            foreach (var item in allItems)
            {
                if (item.Type != _sectionType) continue;

                CreateNewItem(item);
            }
        }
    }
}