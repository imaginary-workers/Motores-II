using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "DataSkinsSO", menuName = "SO/Skins Conteiner", order = 0)]
    public class DataSkinsSO : ScriptableObject
    {
        public Material[] materialesChasis;
        public Color[] colorWheels;
        public Color[] colorGlass;
    }
}