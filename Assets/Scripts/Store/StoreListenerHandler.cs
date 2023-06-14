using ProyectM2.SO;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class StoreListenerHandler : MonoBehaviour
    {
        private static List<IStoreItem> _allItems;

        public static List<IStoreItem> AllItems
        {
            get
            {
                if (_allItems == null)
                {
                    _allItems = FindAllItems();
                }

                return _allItems;
            }
        }

        private static List<IStoreItem> FindAllItems()
        {
            var allItems = new List<IStoreItem>();
            allItems.AddRange(Resources.LoadAll<StoreItemSO>("StoreItems"));

            return allItems;
        }
    }
}
