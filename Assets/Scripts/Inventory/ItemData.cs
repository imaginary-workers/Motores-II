using System;
using UnityEngine;

namespace ProyectM2.Inventory
{
    [Serializable]
    public class ItemData: IItem
    {
        private string uKey = Guid.NewGuid().ToString();
        [SerializeField] private string name;
        [SerializeField, TextArea(3, 10)] private string description;
        [SerializeField] private ItemImage image;
        [SerializeField] private ItemType type;

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

        public ItemImage Image
        {
            get => image;
        }

        public ItemType Type
        {
            get => type;
            set => type = value;
        }
    }
}