using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2
{
    public interface IStoreItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Image { get; }
        public float Price { get; }
        public string Type { get; }
    }
}