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

        public void Right()
        {
            Debug.Log(_inputManager);
            _inputManager.Horizontal(1);
        }

        public void Left()
        {
            Debug.Log(_inputManager);
            _inputManager.Horizontal(-1);
        }

        public void FireBack()
        {
            _inputManager.FireBack();
        }

        public override void Activate()
        {
            Debug.Log("Activate");
            _leftButton?.gameObject.SetActive(true);
            _rightButton?.gameObject.SetActive(true);
            _fireBackButton?.gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            Debug.Log("Deactivate");
            _leftButton?.gameObject.SetActive(false);
            _rightButton?.gameObject.SetActive(false);
            _fireBackButton?.gameObject.SetActive(false);
        }
    }
}