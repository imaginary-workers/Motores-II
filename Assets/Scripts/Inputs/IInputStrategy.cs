using System;
using UnityEngine;

namespace ProyectM2.Inputs
{
    public interface IInputStrategy
    {
        public event Action<int> Horizontal;
        public event Action<Vector3> Click;
        public void OnUpdate();
    }
}
