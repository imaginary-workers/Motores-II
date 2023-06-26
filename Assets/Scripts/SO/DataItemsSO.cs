using System.Collections.Generic;
using ProyectM2.Inventory;
using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "DataSkinsSO", menuName = "SO/Skins Conteiner", order = 0)]
    public class DataItemsSO : ScriptableObject
    {
        public List<MaterialItemData> materialesChassis;
        public List<ColorItemStore> colorWheels;
        public List<ColorItemStore> colorGlass;

        [ContextMenu("AddNewChassis")]
        private void AddNewChassis()
        {
            if (materialesChassis.Count == null)
                materialesChassis = new List<MaterialItemData>();
            var itemData = new MaterialItemData();
            itemData.Type = ItemType.Chassis;
            materialesChassis.Add(itemData);
        }

        [ContextMenu("AddNewWheels")]
        private void AddNewWheels()
        {
            if (colorWheels.Count == null)
                colorWheels = new List<ColorItemStore>();
            var itemData = new ColorItemStore();
            itemData.Type = ItemType.Wheels;
            colorWheels.Add(itemData);
        }

        [ContextMenu("AddNewGlass")]
        private void AddNewGlass()
        {
            if (colorGlass.Count == null)
                colorGlass = new List<ColorItemStore>();
            var itemData = new ColorItemStore();
            itemData.Type = ItemType.Glass;
            colorGlass.Add(itemData);
        }

        public List<MaterialItemData> GetAllChassis()
        {
            return new List<MaterialItemData>(materialesChassis);
        }
        
        public List<ColorItemStore> GetAllWheels()
        {
            return new List<ColorItemStore>(colorWheels);
        }
        
        public List<ColorItemStore> GetAllGlass()
        {
            return new List<ColorItemStore>(colorGlass);
        }

        public List<StoreItem> AllItems()
        {
            var storeItems = new List<StoreItem>();
            storeItems.AddRange(materialesChassis);
            storeItems.AddRange(colorGlass);
            storeItems.AddRange(colorWheels);
            return storeItems;
        }
    }
}