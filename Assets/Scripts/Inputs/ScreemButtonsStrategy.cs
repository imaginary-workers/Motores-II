using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.Inputs
{
    public class ScreemButtonsStrategy : InputStrategy
    {
        public override StrategyType Type => StrategyType.Button;

        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _fireBackButton;

        private void Awake()
        {
            _leftButton.onClick.AddListener(Left);
            _rightButton.onClick.AddListener(Right);
            _fireBackButton.onClick.AddListener(FireBack);
        }

        private void OnDisable()
        {
            _leftButton.onClick.RemoveAllListeners();
            _rightButton.onClick.RemoveAllListeners();
            _fireBackButton.onClick.RemoveAllListeners();
        }

        private void Right()
        {
            _inputManager.Horizontal(1);
        }

        private void Left()
        {
            _inputManager.Horizontal(-1);
        }

        private void FireBack()
        {
            _inputManager.FireBack();
        }
    }
}