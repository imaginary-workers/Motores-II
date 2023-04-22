using System;
using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "Events", menuName = "SO/Events", order = 0)]
    public class Events : ScriptableObject
    {
        public event Action myEvent;

        public void SubscribeToEvent(Action callback)
        {
            myEvent += callback;
        }

        public void InvokeEvent()
        {
            myEvent?.Invoke();
        }
        public void UnsubscribeFromEvent(Action callback)
        {
            myEvent -= callback;
        }
    }
}
