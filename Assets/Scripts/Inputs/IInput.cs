using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public interface IInput
    {
        public event Action<int> Horizontal;
    }
}
