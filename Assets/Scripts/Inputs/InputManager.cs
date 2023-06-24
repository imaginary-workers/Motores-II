using ProyectM2.Gameplay;

namespace ProyectM2.Inputs
{
    public class InputManager : Singleton<InputManager>, IActivatable
    {
        public IInputStrategy Strategy;
        private bool _isActive = false;

        private void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
            
        }

        private void OnDisable()
        {
            ScreenManager.Instance.Subscribe(this);
        }

        private void Update()
        {
            if (CutSceneManager.Instance.IsOnCutScene) return;
            if (!_isActive) return;
            Strategy?.OnUpdate();
        }

        public void SetInputStrategy(IInputStrategy strategy)
        {
            Strategy = strategy;
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
