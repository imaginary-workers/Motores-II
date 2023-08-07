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

        private void Update()
        {
            if (CutSceneManager.Instance.IsOnCutScene)
                Deactivate();
            else
                Activate();
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
        }

        public void Deactivate()
        {
            Strategy?.Deactivate();
        }

        public void Horizontal(int obj)
        {
            OnHorizontal?.Invoke(obj);
        }

        public void Click(Vector3 obj)
        {
            OnClick?.Invoke(obj);
        }

        public void FireBack()
        {
            OnFireBack?.Invoke();
        }
    }
}