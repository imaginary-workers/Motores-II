using UnityEngine;

namespace ProyectM2.Inputs
{
    public class InputManager : MonoBehaviour
    {
        public static IInput CurrentInput = new Swipe();
    }
}
