using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "StoreItemSO", menuName = "SO/Store Item", order = 0)]
    public class StoreItemSO : ScriptableObject, IStoreItem
    {
        public new string name;
        [TextArea(3, 10)] public string description;
        public Sprite image;
        public float price;
        public string type;

        public string Name
        {
            get => name;
        }

        public string Description
        {
            get => description;
        }

        public Sprite Image
        {
            get => image;
        }

        public float Price
        {
            get => price;
        }

        public string Type
        {
            get => type;
        }
    }
}