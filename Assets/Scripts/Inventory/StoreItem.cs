using UnityEngine;

namespace ProyectM2.Inventory
{
    public class StoreItem: ItemData, IPurchable
    {
        [SerializeField] private float price;
        public float Price { get; }
    }
}