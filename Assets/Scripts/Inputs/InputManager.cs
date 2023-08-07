using System;
using ProyectM2.Gameplay;
using UnityEngine;

namespace ProyectM2.Inputs
{
    public class InputManager : Singleton<InputManager>, IActivatable
    {
        public static InputType controlType = InputType.Tactil;
        public event Action<Vector3> OnClick;
        public event Action OnFireBack;
        public event Action<int> OnHorizontal;
        private InputStrategy Strategy;

        public StrategyType StrategyType => Strategy.Type; 

        protected override void Awake()
        {
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
            Strategy.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            Strategy.gameObject.SetActive(false);
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