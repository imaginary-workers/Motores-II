using UnityEngine;

namespace ProyectM2.Inputs
{
    public class InputManager : Singleton<InputManager>
    {
        public IInputStrategy Strategy;

        public void SetInputStrategy(IInputStrategy strategy)
        {
            Strategy = strategy;
            Debug.Log(Strategy);
        }

        private void Update()
        {
            Strategy?.OnUpdate();
        }
    }
}
