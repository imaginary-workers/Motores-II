using ProyectM2.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.Car
{
    public class PlayerPersonalization : MonoBehaviour
    {
        [SerializeField] private GameObject _chassis; 
        [SerializeField] private GameObject _wheels; 
        [SerializeField] private GameObject _windshield;

        private void Start()
        {
            var allItems = InventoryManager.Instance.GetAllItems();
            Debug.Log(allItems.Count);
            foreach (var item in allItems)
            {
                Debug.Log(item);
            }
        }
    }
}
