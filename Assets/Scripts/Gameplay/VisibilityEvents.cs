using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.Gameplay
{
    public class VisibilityEvents : MonoBehaviour
    {
        public UnityEvent OnStart;
        public UnityEvent OnObjectBecameVisible;

        private void OnBecameVisible()
        {
            OnObjectBecameVisible?.Invoke();
        }

        private void Start()
        {
            OnStart?.Invoke();
        }
    }
}