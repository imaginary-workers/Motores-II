using UnityEngine;

namespace ProyectM2.Car
{
    public abstract class Car : MonoBehaviour
    {
        public abstract float Speed { get; }
        public abstract void MoveRight();
        public abstract void MoveLeft();
        public abstract void MoveForward();
    }
}