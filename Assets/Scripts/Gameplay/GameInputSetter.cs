using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class GameInputSetter : MonoBehaviour
    {
        private void Awake()
        {
            InputManager.Instance.SetInputStrategy(new TactilStrategy());
            // if (Application.platform == RuntimePlatform.Android)
            // {
            // }
            // else
            // {
            //     InputManager.Instance.SetInputStrategy(new KeyboardMouseStrategy());
            // }
        }
    }
}