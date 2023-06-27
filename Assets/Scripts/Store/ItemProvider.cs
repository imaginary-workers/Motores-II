using System.Collections.Generic;
using ProyectM2.Inventory;
using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2
{
    public class ItemProvider : Singleton<ItemProvider>
    {
        [SerializeField] private DataItemsSO _itemsSo;

        private DataItemsSO _itemsSO;
 
        public List<StoreItem> AllItems
        {
            get
            {
                return _itemsSo.AllItems();;
            }
        }

        public StoreItem FindSpecificItem(string itemId)
        {
            return AllItems.Find((item) => item.UKey == itemId);
        }
    }
}
