using System;
using UnityEngine;

namespace ProyectM2.Inputs
{
    public class TactilStrategy : InputStrategy
    {
        private Vector2 startTouchPosition;
        private Vector2 endTouchPosition;

        public override StrategyType Type => Inputs.StrategyType.Click;

        public void Update()
        {
            if (!_isActive) return;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                if (Math.Abs((endTouchPosition - startTouchPosition).x) < 100)
                {
                    _inputManager.Click(Input.GetTouch(0).position);
                    return;
                }

                if (endTouchPosition.x < startTouchPosition.x)
                {
                    _inputManager.Horizontal(-1);
                }

                if (endTouchPosition.x > startTouchPosition.x)
                {
                    _inputManager.Horizontal(1);
                }
            }
        }
    }
}