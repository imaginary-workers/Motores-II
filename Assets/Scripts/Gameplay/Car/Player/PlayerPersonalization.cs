using ProyectM2.Inventory;
using ProyectM2.Personalization;
using ProyectM2.Store;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerPersonalization : MonoBehaviour
    {
        [SerializeField] private SetMaterialSkins _materialSkins;
        [SerializeField] private Material _chasisDefault;
        [SerializeField] private Color _wheelsDefault;
        [SerializeField] private Color _glassDefault;

        private void Start()
        {
            UpdateSkinToActive();
        }

        public void UpdateSkinToActive()
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
                        MaterialItemData itemDataM = ItemProvider.Instance.FindMaterialSpecificItem(item.itemID);
                        if (itemDataM != null)
                            _chasisDefault = itemDataM.IObject;
                        else Debug.LogWarning($"Id NOT FOUND: {item.itemID}");
                        break;
                    case ItemType.Glass:
                        ColorItemStore itemDataC = ItemProvider.Instance.ColorSpecificItemGlass(item.itemID);
                        if (itemDataC != null)
                            _glassDefault = itemDataC.IObject;
                        else Debug.LogWarning($"Id NOT FOUND: {item.itemID}");
                        break;
                    case ItemType.Wheels:
                        ColorItemStore itemDataCW = ItemProvider.Instance.ColorSpecificItemWheels(item.itemID);
                        if (itemDataCW != null)
                            _wheelsDefault = itemDataCW.IObject;
                        else Debug.LogWarning($"Id NOT FOUND: {item.itemID}");
                        break;
                    default:
                        Debug.LogWarning($"Id Type DOES'NT EXIST: Type->{item.itemType} | id -> {item.itemID}");
                        break;
                }
            }

            _materialSkins.SetMaterialChasis(_chasisDefault);
            _materialSkins.SetColorGlass(_glassDefault);
            _materialSkins.SetColorWheels(_wheelsDefault);
        }
    }
}
