using ProyectM2.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class CarPersonalization : MonoBehaviour
    {
        [SerializeField] Material _chasis;
        [SerializeField] Color _wheels;
        [SerializeField] Color _glass;
        [SerializeField] GameObject _carChasis;
        [SerializeField] GameObject[] _carWheels;
        [SerializeField] GameObject _carGlass;

        private void Start()
        {
            var allItems = InventoryManager.Instance.GetAllItems();
            
        }
    }
}
