using ProyectM2.Inventory;

namespace ProyectM2.Personalization
{
    public class InventorySectionUI : SectionUI
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