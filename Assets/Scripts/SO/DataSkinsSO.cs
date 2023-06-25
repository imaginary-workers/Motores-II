using System;
using System.Collections.Generic;
using ProyectM2.Inventory;
using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "DataSkinsSO", menuName = "SO/Skins Conteiner", order = 0)]
    public class DataSkinsSO : ScriptableObject
    {
        public List<ItemData<Material>> materialesChassis;
        public List<ItemData<Color>> colorWheels;
        public List<ItemData<Color>> colorGlass;

        [ContextMenu("AddNewChassis")]
        public void AddNewChassis()
        {
            if (materialesChassis.Count == null)
                materialesChassis = new List<ItemData<Material>>();
            var itemData = new ItemData<Material>();
            itemData.Type = ItemType.Chassis;
            materialesChassis.Add(itemData);
        }

        [ContextMenu("AddNewWheels")]
        public void AddNewWheels()
        {
            if (colorWheels.Count == null)
                colorWheels = new List<ItemData<Color>>();
            var itemData = new ItemData<Color>();
            itemData.Type = ItemType.Wheels;
            colorWheels.Add(itemData);
        }

        [ContextMenu("AddNewGlass")]
        public void AddNewGlass()
        {
            if (colorGlass.Count == null)
                colorGlass = new List<ItemData<Color>>();
            var itemData = new ItemData<Color>();
            itemData.Type = ItemType.Glass;
            colorGlass.Add(itemData);
        }
    }

    [Serializable]
    public class ItemData<T>: IStoreItem
    {
        private string uKey = Guid.NewGuid().ToString();
        [SerializeField]private T iObject;
        [SerializeField]private string name;
        [SerializeField, TextArea(3, 10)] private string description;
        [SerializeField]private Sprite image;
        [SerializeField]private float price;
        [SerializeField] private ItemType type;
        [SerializeField, Tooltip("Para los items que no lleven imagen")]
        private Color color;

        public string UKey
        {
            get => uKey;
        }

        public string Name
        {
            get => name;
        }

        public string Description
        {
            get => description;
        }

        public Sprite Image
        {
            get => image;
        }

        public Color Color
        {
            get => color;
        }

        public float Price
        {
            get => price;
        }

        public ItemType Type
        {
            get => type;
            set => type = value;
        }
    }
}