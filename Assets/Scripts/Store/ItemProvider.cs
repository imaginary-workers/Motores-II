using System.Collections.Generic;
using ProyectM2.Inventory;
using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2
{
    public class ItemProvider : Singleton<ItemProvider>
    {
        [SerializeField] DataItemsSO _itemsSo;
 
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
        public List<MaterialItemData> AllChasisItem
        {
            get
            {
                return _itemsSo.GetAllChassis();
            }
        }
        public MaterialItemData FindMaterialSpecificItem(string itemId)
        {
            return AllChasisItem.Find((item) => item.UKey == itemId);
        }

        public List<ColorItemStore> AllWheelsItem
        {
            get
            {
                return _itemsSo.GetAllWheels();
            }
        }
        public ItemStoreGeneric<Color> ColorSpecificItemWheels(string itemId)
        {
            return AllWheelsItem.Find((item) => item.UKey == itemId);
        }

        public List<ColorItemStore> AllGlassItem
        {
            get
            {
                return _itemsSo.GetAllGlass();
            }
        }
        public ItemStoreGeneric<Color> ColorSpecificItemGlass(string itemId)
        {
            return AllGlassItem.Find((item) => item.UKey == itemId);
        }
    }
}
