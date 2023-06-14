using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "StoreItemSO", menuName = "SO/Store Item", order = 0)]
    public class StoreItemSO : ScriptableObject
    {
        public string name;
        public string description;
        public Image image;
        public float price;
        public string type;
    }
}