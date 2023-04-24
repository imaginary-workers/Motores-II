using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public interface IObservable
    {
        void Suscribe(IObserver obs);
        void NotifyToObservers(string obs);
        void Unsuscribe(IObserver obs);
    }
}
