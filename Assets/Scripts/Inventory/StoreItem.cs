using UnityEngine;
using System;

namespace ProyectM2.Inventory
{
    [Serializable]
    public class StoreItem: ItemData, IPurchable
    {
        [SerializeField] private float price;
        public float Price { get => price; }
    }
}