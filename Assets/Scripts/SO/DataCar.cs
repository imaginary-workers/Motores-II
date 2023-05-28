using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "CarData", menuName = "SO/Car Data", order = 0)]
    public class DataCar : ScriptableObject
    {
        [field: SerializeField, Range(1, 30)] public float speedRotation { get; private set; } = 15f;
        [field: SerializeField, Range(1, 10)] public float horizontalRange { get; private set; } = 3.22f;
        [field: SerializeField, Range(1, 50)] public float speedForward { get; private set; } = 27f;
        [field: SerializeField, Range(1, 30)] public float speedHorizontal { get; private set; } = 10f;
    }
}