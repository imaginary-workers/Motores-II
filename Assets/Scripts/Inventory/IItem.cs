using System;
using ProyectM2.Inventory;
using UnityEngine;

namespace ProyectM2
{
    public interface IItem
    {
        public string UKey { get; }
        public string Name { get; }
        public string Description { get; }
        public ItemImage Image { get; }
        public ItemType Type { get; }
    }

    [Serializable]
    public class ItemImage
    {
        public Color color;
        public Sprite sprite;
    }
}