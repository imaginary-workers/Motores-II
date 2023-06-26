using ProyectM2.Personalization;

namespace ProyectM2.UI.Store
{
    public class StoreSectionUI : SectionUI
    {
        protected override void SetAllItems()
        {
            var allItems = ItemProvider.Instance.AllItems;
            foreach (var item in allItems)
            {
                if (item.Type != _sectionType) continue;

                CreateNewItem(item);
            }
        }
    }
}