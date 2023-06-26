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
        public List<StoreItem> powerUps;

        [ContextMenu("AddNewChassis")]
        private void AddNewChassis()
        {
            if (materialesChassis == null)
                materialesChassis = new List<MaterialItemData>();
            var itemData = new MaterialItemData();
            itemData.Type = ItemType.Chassis;
            materialesChassis.Add(itemData);
        }

        [ContextMenu("AddNewWheels")]
        private void AddNewWheels()
        {
            if (colorWheels == null)
                colorWheels = new List<ColorItemStore>();
            var itemData = new ColorItemStore();
            itemData.Type = ItemType.Wheels;
            colorWheels.Add(itemData);
        }

        [ContextMenu("AddNewGlass")]
        private void AddNewGlass()
        {
            if (colorGlass == null)
                colorGlass = new List<ColorItemStore>();
            var itemData = new ColorItemStore();
            itemData.Type = ItemType.Glass;
            colorGlass.Add(itemData);
        }

        [ContextMenu("AddNewPowerUp")]
        private void AddNewPowerUp()
        {
            if (powerUps == null)
                powerUps = new List<StoreItem>();
            var itemData = new StoreItem();
            itemData.Type = ItemType.PowerUp;
            powerUps.Add(itemData);
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

        public List<StoreItem> GetAllPowerUp()
        {
            return new List<StoreItem>(powerUps);
        }

        public List<StoreItem> AllItems()
        {
            var storeItems = new List<StoreItem>();
            storeItems.AddRange(materialesChassis);
            storeItems.AddRange(colorGlass);
            storeItems.AddRange(colorWheels);
            storeItems.AddRange(powerUps);
            return storeItems;
        }
    }
}