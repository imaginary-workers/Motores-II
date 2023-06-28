using ProyectM2.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class ActivationItem : MonoBehaviour
    {      

      public void ActivateItem(ItemData Item)
        {
            EventManager.TriggerEvent("ActiveItem", Item.UKey);
            Debug.Log("Quiero ser negro");
        }
    }
}
