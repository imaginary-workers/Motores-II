using System;

namespace ProyectM2.Inputs
{
    public interface IInput
    {
        public event Action<int> Horizontal;
        public void OnUpdate();
    }
}
