using ProyectM2.SO;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class StoreListenerHandler : MonoBehaviour
    {
        private static List<StoreItemSO> _allItems;

        public static List<StoreItemSO> AllItems
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

        private static List<StoreItemSO> FindAllItems()
        {
            var allItems = new List<StoreItemSO>();
            allItems.AddRange(Resources.LoadAll<StoreItemSO>("StoreItems"));

            return allItems;
        }
    }
}
