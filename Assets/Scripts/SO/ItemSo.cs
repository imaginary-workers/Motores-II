using ProyectM2.Inventory;
using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "StoreItemSO", menuName = "SO/Store Item", order = 0)]
    public class ItemSo : ScriptableObject, IItem
    {
        [SerializeField] private new string name;
        [SerializeField, TextArea(3, 10)] private string description;
        [SerializeField] private ItemImage image;
        [SerializeField] private float price;
        [SerializeField] private ItemType type;
        [SerializeField, Tooltip("Para los items que no lleven imagen")]
        private Color color;

        public string UKey { get; }

        public string Name
        {
            get => name;
        }

        public string Description
        {
            get => description;
        }

        public ItemImage Image
        {
            get => image;
        }

        public Color Color
        {
            get => color;
        }

        public float Price
        {
            get => price;
        }

        public ItemType Type
        {
            get => type;
        }
    }
}