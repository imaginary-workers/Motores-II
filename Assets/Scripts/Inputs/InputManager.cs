using UnityEngine;

namespace ProyectM2.Inputs
{
    public class InputManager : Singleton<InputManager>
    {
        protected override void Awake()
        {
            itDestroyOnLoad = true;
            base.Awake();
        }

        public IInputStrategy Strategy;

        public void SetInputStrategy(IInputStrategy strategy)
        {
            Strategy = strategy;
        }

        private void Update()
        {
            Strategy?.OnUpdate();
        }
    }
}
