using UnityEngine;

namespace ProyectM2.Inputs
{
    public class KeyboardMouseStrategy : InputStrategy
    {
        public override StrategyType Type => StrategyType.Both;
        public void Update()
        {
            if (!_isActive) return;
            if (Input.GetKeyDown(KeyCode.A))
            {
                _inputManager.Horizontal(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _inputManager.Horizontal(1);
            }

            if (Input.GetMouseButtonDown(0))
            {
                _inputManager.Click(Input.mousePosition);
            }
        }

    }
}