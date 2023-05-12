using System;
using UnityEngine;

namespace ProyectM2.Inputs
{
    public class KeyboardMouseStrategy : IInputStrategy
    {
        public event Action<int> Horizontal;
        public event Action<Vector3> Click;

        public void OnUpdate()
        {

            if (Input.GetKeyDown(KeyCode.A))
            {
                Horizontal?.Invoke(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Horizontal?.Invoke(1);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Click?.Invoke(Input.mousePosition);
            }

        }
    }
}