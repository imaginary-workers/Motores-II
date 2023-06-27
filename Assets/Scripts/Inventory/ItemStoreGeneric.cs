using System;
using UnityEngine;

namespace ProyectM2.Inventory
{
    [Serializable]
    public class ItemStoreGeneric<T>: StoreItem
    {
        [SerializeField] private T iObject;

        public T IObject { get => iObject; set => iObject = value; }
    }
}