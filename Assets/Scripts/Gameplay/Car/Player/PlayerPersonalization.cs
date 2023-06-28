using ProyectM2.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.Car
{
    public class PlayerPersonalization : MonoBehaviour
    {
        [SerializeField] SetMaterialSkins _materialSkins;
        [SerializeField] Material _chasisDefault;
        [SerializeField] Color _wheelsDefault;
        [SerializeField] Color _glassDefault;

        private void Start()
        {
            var allItems = InventoryManager.Instance.GetAllItems();
            foreach (var item in allItems)
            {
                if (!item.isActive) continue;
                switch (item.itemType)
                {
                    case ItemType.PowerUp:
                        break;
                    case ItemType.Chassis:
                        _chasisDefault = ItemProvider.Instance.FindMaterialSpecificItem(item.itemID).IObject;
                        break;
                    case ItemType.Glass:
                        _glassDefault = ItemProvider.Instance.ColorSpecificItemGlass(item.itemID).IObject;
                        break;
                    case ItemType.Wheels:
                        _wheelsDefault = ItemProvider.Instance.ColorSpecificItemWheels(item.itemID).IObject;
                        break;
                    default:
                        break;
                }
            }
            _materialSkins.SetMaterialChasis(_chasisDefault);
            _materialSkins.SetColorGlass(_glassDefault);
            _materialSkins.SetColorWheels(_wheelsDefault);
        }
    }
}
