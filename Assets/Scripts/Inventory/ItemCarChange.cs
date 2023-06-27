using ProyectM2.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class ItemCarChange : MonoBehaviour
    {
        [SerializeField] SetMaterialSkins SetMaterialSkins;

        private void Start()
        {

        }

        public void MaterialCarChange(ItemData Itemdata)
        {
            var _uKey = Itemdata.UKey;
            var _type = Itemdata.Type;
            var _item = ItemProvider.Instance.FindSpecificItem(_uKey);

            if (_type == ItemType.Chassis)
            {
                var _itemMaterial = (MaterialItemData)_item;
                SetMaterialSkins.SetMaterialChasis(_itemMaterial.IObject);
            }
            else if (_type == ItemType.Wheels || _type == ItemType.Glass)
            {
                var _itemColor = (ColorItemStore)_item;
                if (_type == ItemType.Glass)
                {
                    SetMaterialSkins.SetColorGlass(_itemColor.IObject);
                }
                else if (_type == ItemType.Wheels)
                {

                    SetMaterialSkins.SetColorWheels(_itemColor.IObject);
                }
            }
        }

    }
}
