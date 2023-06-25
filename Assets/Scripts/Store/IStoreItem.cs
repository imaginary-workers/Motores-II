using ProyectM2.Inventory;
using UnityEngine;

namespace ProyectM2
{
    public interface IStoreItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Image { get; }
        public float Price { get; }
        public ItemType Type { get; }
    }
}