using ProyectM2.Inventory;
using UnityEngine;

namespace ProyectM2.Personalization
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
