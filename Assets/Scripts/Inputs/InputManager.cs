using System;
using ProyectM2.Gameplay;
using UnityEngine;

namespace ProyectM2.Inputs
{
    public class InputManager : Singleton<InputManager>, IActivatable
    {
        public static InputType controlType = InputType.ScreenButton;
        public event Action<Vector3> OnClick;
        public event Action OnFireBack;
        public event Action<int> OnHorizontal;
        private InputStrategy Strategy;
        private bool _isActive = false;

        public StrategyType StrategyType => Strategy.Type; 

        protected override void Awake()
        {
            itDestroyOnLoad = true;
            base.Awake();
            SetInputStrategy(InputStrategyFactory.Instance.CreateInputStrategy(controlType));
        }

        private void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            ScreenManager.Instance.Subscribe(this);
        }

        public void SetInputStrategy(InputStrategy strategy)
        {
            if (Strategy != null)
            {
                Destroy(Strategy.gameObject);
            }

            Strategy = strategy;
            Strategy.SetManager(Instance);
        }

        public void Activate()
        {
            Strategy?.Activate();
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
            Strategy?.Deactivate();
        }

        public void Horizontal(int obj)
        {
            if (!_isActive) return;
            OnHorizontal?.Invoke(obj);
        }

        public void Click(Vector3 obj)
        {
            if (!_isActive) return;
            OnClick?.Invoke(obj);
        }

        public void FireBack()
        {
            if (!_isActive) return;
            OnFireBack?.Invoke();
        }
    }
}