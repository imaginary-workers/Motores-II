using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Managers
{
    public class InputManager : MonoBehaviour
    {
        public static IInput CurrentInput = new Swipe();
    }
}
