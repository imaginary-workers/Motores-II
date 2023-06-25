using System;
using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "Int", menuName = "SO/Int", order = 0)]

    public class DataIntObservable : ScriptableObject
    {
        public event Action observers;
        [SerializeField] protected int _value = 1;

        public virtual int value
        {
            get => _value;
            set
            {
                observers?.Invoke();
                _value = value;
            } 
        }

        public void Subscribe(Action observer)
        {
            observers += observer;
        }
        
        public void Unsubscribe(Action observer)
        {
            observers -= observer;
        }
    }
}