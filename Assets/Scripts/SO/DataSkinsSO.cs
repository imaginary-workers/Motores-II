using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "DataSkinsSO", menuName = "SO/Skins Conteiner", order = 0)]
    public class DataSkinsSO : ScriptableObject
    {
        public List<ItemData<Material>> materialesChassis;
        public List<ItemData<Color>> colorWheels;
        public List<ItemData<Color>> colorGlass;

        [ContextMenu("AddNewChassis")]
        public void AddNewChassis()
        {
            if (materialesChassis.Count == null)
                materialesChassis = new List<ItemData<Material>>();
            materialesChassis.Add(new ItemData<Material>());
        }

        [ContextMenu("AddNewWheels")]
        public void AddNewWheels()
        {
            if (colorWheels.Count == null)
                colorWheels = new List<ItemData<Color>>();
            colorWheels.Add(new ItemData<Color>());
        }
        [ContextMenu("AddNewGlass")]
        public void AddNewGlass()
        {
            if (colorGlass.Count == null)
                colorGlass = new List<ItemData<Color>>();
            colorGlass.Add(new ItemData<Color>());
        }
    }

    [Serializable]
    public class ItemData<T>
    {
        private string uKey = Guid.NewGuid().ToString();
        public T iObject;
    }
}