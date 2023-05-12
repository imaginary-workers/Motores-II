using System;
using UnityEngine;

namespace ProyectM2.Inputs
{
    public class TactilStrategy: IInputStrategy
    {
        Vector2 startTouchPosition;
        Vector2 endTouchPosition;

        public event Action<int> Horizontal;
        public event Action<Vector3> Click;

        public void OnUpdate()
        {
            HorizontalSwipe();
        }

        public void HorizontalSwipe()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                if (Math.Abs((endTouchPosition - startTouchPosition).x) < 100)
                {
                    Click?.Invoke(Input.GetTouch(0).position);
                    return;
                }

                if (endTouchPosition.x < startTouchPosition.x)
                {                   
                    Horizontal?.Invoke(-1);
                }

                if (endTouchPosition.x > startTouchPosition.x)
                {
                    Horizontal?.Invoke(1);
                }
            }
        }
    }
}


