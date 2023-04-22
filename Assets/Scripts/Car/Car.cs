using UnityEngine;

namespace ProyectM2.Car
{
    public abstract class Car : MonoBehaviour
    {
        //[SerializeField] protected AnimManager myAnim;
        public abstract AnimManager MyAnim { get; }
        public abstract float Speed { get; }
        public abstract void MoveRight();
        public abstract void MoveLeft();
        public abstract void MoveForward();
    }
}