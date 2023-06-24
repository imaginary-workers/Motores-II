using ProyectM2.Gameplay;

namespace ProyectM2.Inputs
{
    public class InputManager : Singleton<InputManager>, IActivatable
    {
        public IInputStrategy Strategy;
        private bool _isActive = false;

        public void SetInputStrategy(IInputStrategy strategy)
        {
            Strategy = strategy;
        }

        private void Update()
        {
            if (_isActive) return;
            Strategy?.OnUpdate();
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }
    }
}
