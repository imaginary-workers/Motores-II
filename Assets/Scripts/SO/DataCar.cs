using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "CarData", menuName = "SO/Car Data", order = 0)]
    public class DataCar : ScriptableObject
    {
        [field: SerializeField, Range(1, 5)] public float horizontalRange { get; private set; } = 3.22f;
        [field: SerializeField, Range(1, 20)] public float speed { get; private set; } = 1f;
    }
}