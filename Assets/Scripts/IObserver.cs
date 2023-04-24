using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public interface IObserver
    {
        void Notify(string action);
    }
}
