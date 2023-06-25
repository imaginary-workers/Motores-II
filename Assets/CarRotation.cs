using UnityEngine;

namespace ProyectM2
{
    public class CarRotation : MonoBehaviour
    {
        public float rotationSpeed = 100f;

        void Update()
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
        }
    }
}