using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class GameInputSetter : MonoBehaviour
    {
        private void Awake()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                InputManager.Instance.SetInputStrategy(new TactilStrategy());
            }
            else
            {
                InputManager.Instance.SetInputStrategy(new KeyboardMouseStrategy());
            }
        }
    }
}